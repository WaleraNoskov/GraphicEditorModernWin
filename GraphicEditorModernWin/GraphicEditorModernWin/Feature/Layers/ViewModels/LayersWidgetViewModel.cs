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
        RemoveLayerCommand = new RelayCommand<Guid>(OnRemoveLayerCommandExecuted, CanRemoveCommandExecute);
	}

    public ObservableCollection<LayerViewModel> Layers { get; } = [];

    private LayerViewModel? _selectedLayer;
    public LayerViewModel? SelectedLayer
    {
        get => _selectedLayer;
        set
        {
            SetField(ref _selectedLayer, value);
            RemoveLayerCommand.RaiseCanExecuteChanged();
        }
    }

    public ICommand RestoreLayersCommand { get; private set; }
    private void OnRestoreLayersCommandExecuted()
    {
        RestoreLayers();
	}

	public ICommand AddLayerCommand { get; private set; }
    private void OnAddLayer()
    {
        _layersService.AddLayer(new Core.ValueTypes.Bgra(0,0,0,0));
    }

    public RelayCommand<Guid> RemoveLayerCommand { get; private set; }
    private void OnRemoveLayerCommandExecuted(Guid id)
    {
        _layersService.RemoveLayer(id);
    }
    private bool CanRemoveCommandExecute(Guid id) => SelectedLayer is not null;

    private void RestoreLayers()
    {
		var orderedLayers = _layersService.Layers
			.OrderByDescending(l => l.Order)
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
