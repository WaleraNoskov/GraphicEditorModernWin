using System.Collections.Generic;
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
	}

    private List<Bgra> _colors = [];
    public IReadOnlyList<Bgra> Colors => _colors.AsReadOnly();

    public Bgra PrimaryColor => _brushStateService.PrimaryColor;
    public Bgra SecondaryColor => _brushStateService.SecondaryColor;

    public void Initialize()
    {
        _colors = [
            new Bgra(0, 0, 0, 255),
            new Bgra(255, 255, 255, 255),
            new Bgra(255, 0, 0, 255),
            new Bgra(0, 255, 0, 255),
            new Bgra(0, 0, 255, 255)
        ];
        OnPropertyChanged(nameof(Colors));
	}

    public void SetPrimaryColor(Bgra color) => _brushStateService.PrimaryColor = color;

    public void SetSecondaryColor(Bgra color) => _brushStateService.SecondaryColor = color;


}
