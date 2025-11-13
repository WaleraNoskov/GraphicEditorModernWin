using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GraphicEditorModernWin.Core.Entities;
using GraphicEditorModernWin.Core.Services;
using GraphicEditorModernWin.Feature.Shared.Framework;
using GraphicEditorModernWin.Feature.Shared.Framework.Commands;

namespace GraphicEditorModernWin.Feature.Layers.ViewModels;

internal class LayersWidgetViewModel : NotifyPropertyChangedBase
{
    private readonly ILayersService _layersService;

    public LayersWidgetViewModel(ILayersService layersService)
    {
        _layersService = layersService;
        _layersService.LayersChanged += (s, e) => RestoreLayers();

		RestoreLayersCommand = new RelayCommand(OnRestoreLayersCommandExecuted);
        AddLayerCommand = new RelayCommand(OnAddLayer);
        RemoveLayerCommand = new RelayCommand<Guid>(OnRemoveLayerCommandExecuted);
	}

    public ObservableCollection<LayerViewModel> Layers { get; } = [];

    private LayerViewModel? _selectedLayer;
    public LayerViewModel? SelectedLayer
    {
        get => _selectedLayer;
        set => SetField(ref _selectedLayer, value);
	}

	public ICommand RestoreLayersCommand { get; private set; }
    private void OnRestoreLayersCommandExecuted()
    {
        RestoreLayers();
	}

	public ICommand AddLayerCommand { get; private set; }
    private void OnAddLayer()
    {
        var layer = new Layer(new Core.ValueTypes.Size(800, 600));
        _layersService.AddLayer(layer);
    }

    public ICommand RemoveLayerCommand { get; private set; }
    private void OnRemoveLayerCommandExecuted(Guid id)
    {
        _layersService.RemoveLayer(id);
    }

    private void RestoreLayers()
    {
		var orderedLayers = _layersService.Layers
			.Join(_layersService.LayersOrder, l => l.Id, o => o.Value, (l, o) => new { Layer = l, Order = o.Key })
			.OrderBy(l => l.Order)
			.Select(l => l.Layer)
			.ToList();

		Layers.Clear();
		foreach (var layer in orderedLayers)
		{
			Layers.Add(new LayerViewModel(layer));
		}

        if (Layers.Count > 0)
            SelectedLayer = Layers.Last();
	}
}
