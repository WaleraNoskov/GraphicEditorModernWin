using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GraphicEditorModernWin.Core.ValueTypes;
using GraphicEditorModernWin.Feature.Shared.Framework;
using GraphicEditorModernWin.Feature.Shared.Services;

namespace GraphicEditorModernWin.Feature.ColorPalete.Busines;

internal class ColorPaletteModel : NotifyPropertyChangedBase
{
    private readonly BrushStateService _brushStateService;

    public ColorPaletteModel(BrushStateService brushStateService)
    {
        _brushStateService = brushStateService;
        _brushStateService.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);

        _colors = [];
        Colors = new ReadOnlyObservableCollection<Bgra>(_colors);
	}

	private ObservableCollection<Bgra> _colors;
	public ReadOnlyObservableCollection<Bgra> Colors;

    public Bgra PrimaryColor => _brushStateService.PrimaryBrush;
    public Bgra SecondaryColor => _brushStateService.SecondaryBrush;

    public void Initialize()
    {
        _colors =
        [
            new Bgra(0, 0, 0, 1),
            new Bgra(255, 255, 255, 1),
            new Bgra(255, 0, 0, 1),
            new Bgra(0, 255, 0, 1),
            new Bgra(0, 0, 255, 1),
        ];
    }

    public void SetPrimaryColor(Bgra color) => _brushStateService.PrimaryBrush = color;

    public void SetSecondaryColor(Bgra color) => _brushStateService.SecondaryBrush = color;


}
