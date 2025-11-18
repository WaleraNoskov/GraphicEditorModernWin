using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GraphicEditorModernWin.Core.Entities;
using GraphicEditorModernWin.Core.Services;
using GraphicEditorModernWin.Core.ValueTypes;
using GraphicEditorModernWin.Feature.Shared.Framework;
using GraphicEditorModernWin.Feature.Shared.Framework.Commands;
using GraphicEditorModernWin.StandartPack.Command;
using Microsoft.Graphics.Canvas;
using OpenCvSharp;

namespace GraphicEditorModernWin.Feature.Drawing.ViewModel;

internal class RenderLayerViewModel : NotifyPropertyChangedBase
{
	private readonly ICommandManager _commandManager;
	private readonly IColorPaletteService _colorPaletteService;
	private Layer _layer;

    public RenderLayerViewModel(Layer layer, ICommandManager commandManager, IColorPaletteService colorPaletteService)
	{
		_commandManager = commandManager;
		_colorPaletteService = colorPaletteService;
		
		_layer = layer;
		_bitmap = _layer.Drawing;

		CommitStrokeCommand = new RelayCommand<List<Vector2>>(OnCommitStrokeCommandExecuted);
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

    public double ZoomedWidth => Bitmap.Width * Zoom;
	public double ZoomedHeight => Bitmap.Height * Zoom;
	public Bgra PrimaryColor => _colorPaletteService.PrimaryColor;

	public RelayCommand<List<Vector2>> CommitStrokeCommand { get; private set; }
	private void OnCommitStrokeCommandExecuted(List<Vector2>? stroke)
	{
		if (stroke is null)
			return;

		var coreStroke = stroke.Select(v => new Core.ValueTypes.Position((int)v.X, (int)v.Y)).ToList();
		var command = new StrokeCommand(_layer.Id, coreStroke, 1, _colorPaletteService.PrimaryColor);

		_commandManager.Invoke(command);
	}
}
