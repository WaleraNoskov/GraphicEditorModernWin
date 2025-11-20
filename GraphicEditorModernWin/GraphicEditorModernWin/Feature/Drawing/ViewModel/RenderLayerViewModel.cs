using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using GraphicEditorModernWin.Core.Contracts;
using GraphicEditorModernWin.Core.Entities;
using GraphicEditorModernWin.Core.Services;
using GraphicEditorModernWin.Core.ValueTypes;
using GraphicEditorModernWin.Feature.Drawing.Contracts;
using GraphicEditorModernWin.Feature.Shared.Framework;
using GraphicEditorModernWin.Feature.Shared.Framework.Commands;
using GraphicEditorModernWin.StandartPack.Command;
using OpenCvSharp;

namespace GraphicEditorModernWin.Feature.Drawing.ViewModel;

internal class RenderLayerViewModel : NotifyPropertyChangedBase
{
    private readonly ICommandManager _commandManager;
    private readonly IColorPaletteService _colorPaletteService;
    private readonly ILayersService _layersService;
    private Layer _layer;

    public RenderLayerViewModel(Layer layer, ICommandManager commandManager, IColorPaletteService colorPaletteService, ILayersService layersService)
    {
        _commandManager = commandManager;
        _colorPaletteService = colorPaletteService;
        _layersService = layersService;
        _layersService.LayerChanged += _layersService_LayerChanged;

        _layer = layer;
        _bitmap = _layer.Drawing;

        CommitCommand = new RelayCommand<ICommitParameters>(OnCommitCommandExecuted);
    }

    private Mat _bitmap;
    public Mat Bitmap
    {
        get => _bitmap;
        private set => SetField(ref _bitmap, value);
    }

    private double _zoom = 1;
    public double Zoom
    {
        get => _zoom;
        set
        {
            SetField(ref _zoom, value);
            OnPropertyChanged(nameof(ZoomedWidth));
            OnPropertyChanged(nameof(ZoomedHeight));
        }
    }

    private Instruments _currentInstrument;
    public Instruments CurrentInstrument
    {
        get => _currentInstrument;
        set => SetField(ref _currentInstrument, value);
    }

    public double ZoomedWidth => Bitmap.Width * Zoom;
    public double ZoomedHeight => Bitmap.Height * Zoom;
    public Bgra PrimaryColor => _colorPaletteService.PrimaryColor;
    public Guid LayerId => _layer.Id;

    public event EventHandler? LayerChanged;

    public RelayCommand<ICommitParameters> CommitCommand { get; private set; }
    private void OnCommitCommandExecuted(ICommitParameters? commit)
    {
        ICommand? command = commit switch
        {
            StrokeCommitParameters strokeCommitParameters => new DrawStrokeCommand(_layer.Id, strokeCommitParameters.Stroke, 1, _colorPaletteService.PrimaryColor),
            RectangleCommitParameters rectangleCommitParameters => new DrawRectangleCommand(_layer.Id,
                                                                                            1,
                                                                                            rectangleCommitParameters.Rectangle,
                                                                                            rectangleCommitParameters.DrawFrame ? _colorPaletteService.PrimaryColor : null,
                                                                                            rectangleCommitParameters.FillFrame ? _colorPaletteService.SecondaryColor : null),
            _ => null
        };

        if (command is not null)
            _commandManager.Invoke((dynamic)command);
    }

    private void _layersService_LayerChanged(object? sender, Guid e)
    {
        var newLayer = _layersService.GetLayerById(e);
        if (newLayer is null)
            return;

        _layer = newLayer;
        LayerChanged?.Invoke(this, EventArgs.Empty);
    }
}
