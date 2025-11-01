using System;
using System.Collections.Generic;
using GraphicEditorModernWin.Core.ValueTypes;
using GraphicEditorModernWin.Feature.Shared.Services;

namespace GraphicEditorModernWin.Feature.ColorPalete.Busines;

internal class ColorPaleteModel
{
    private readonly BrushStateService _brushStateService;

    private List<Bgra> _colors = [];

    public ColorPaleteModel(BrushStateService brushStateService)
    {
        _brushStateService = brushStateService;

        _brushStateService.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(BrushStateService.PrimaryBrush))
                PrimaryColorChanged?.Invoke(this, EventArgs.Empty);
            else if (e.PropertyName == nameof(BrushStateService.SecondaryBrush))
                SecondaryColorChanged?.Invoke(this, EventArgs.Empty);
        };
    }

    public IReadOnlyList<Bgra> Colors => _colors.AsReadOnly();
    public Bgra PrimaryColor => _brushStateService.PrimaryBrush;
    public Bgra SecondaryColor => _brushStateService.SecondaryBrush;

    public event EventHandler? PrimaryColorChanged;
    public event EventHandler? SecondaryColorChanged;

    public void Initialize()
    {
        _colors.AddRange(
        [ 
            new Bgra(0, 0, 0, 1),
            new Bgra(255, 255, 255, 1),
            new Bgra(255, 0, 0, 1),
            new Bgra(0, 255, 0, 1),
            new Bgra(0, 0, 255, 1),
        ]);
    }

    public void SetPrimaryColor(Bgra color) => _brushStateService.PrimaryBrush = color;

    public void SetSecondaryColor(Bgra color) => _brushStateService.SecondaryBrush = color;


}
