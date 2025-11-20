using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GraphicEditorModernWin.Core.Services;
using GraphicEditorModernWin.Feature.Drawing.Contracts;
using GraphicEditorModernWin.Feature.Shared.Framework;
using GraphicEditorModernWin.Feature.Shared.Framework.Commands;

namespace GraphicEditorModernWin.Feature.Drawing.ViewModel;

internal class RenderViewModel : NotifyPropertyChangedBase
{
    private readonly ILayersService _layersService;
    private readonly ICommandManager _commandManager;
    private readonly IColorPaletteService _colorPaletteService;

    public RenderViewModel(ILayersService layersService, ICommandManager commandManager, IColorPaletteService colorPaletteService)
    {
        _layersService = layersService;
        _layersService.LayersChanged += (_, _) => RestoreLayers();

        _commandManager = commandManager;

        _colorPaletteService = colorPaletteService;

        InitializeCommand = new RelayCommand(OnInitializeCommandExecuted);
        SetZoomCommand = new RelayCommand<double>(OnSetZoomCommandExecuted);
        SetInstrumentCommand = new RelayCommand<Instruments>(OnSetInstrumentCommandExecuted);
    }

    private double _zoom = 1;
    public double Zoom
    {
        get => _zoom;
        private set => SetField(ref _zoom, value);
    }

    private Instruments _instrument;
    public Instruments Instrument
    {
        get => _instrument;
        private set => SetField(ref _instrument, value);
    }

    private readonly List<RenderLayerViewModel> _layers = [];
    public IReadOnlyCollection<RenderLayerViewModel> Layers => _layers.AsReadOnly();

    public ICommand InitializeCommand { get; private set; }
    private void OnInitializeCommandExecuted() => RestoreLayers();

    public RelayCommand<double> SetZoomCommand { get; private set; }
    private void OnSetZoomCommandExecuted(double zoom)
    {
		Zoom = zoom;

		foreach (var layer in _layers)
			layer.Zoom = zoom;
	}

    public RelayCommand<Instruments> SetInstrumentCommand { get; private set; }
    private void OnSetInstrumentCommandExecuted(Instruments instrument)
    {
        Instrument = instrument;

        foreach (var layer in Layers) 
            layer.CurrentInstrument = instrument;
    }

    private void RestoreLayers()
    {
        var layerViewModels = _layersService.Layers
            .OrderBy(l => l.Order)
            .Select(l => new RenderLayerViewModel(l, _commandManager, _colorPaletteService, _layersService))
            .ToList();

        _layers.Clear();
        foreach (var layer in layerViewModels)
            _layers.Add(layer);

        OnPropertyChanged(nameof(Layers));
    }
}
