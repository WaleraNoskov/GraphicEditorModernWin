using GraphicEditorModernWin.Core.ValueTypes;
using GraphicEditorModernWin.Feature.Shared.Framework;

namespace GraphicEditorModernWin.Feature.Shared.Services;

internal class BrushStateService : NotifyPropertyChangedBase
{
    private Bgra _bgra;
    public Bgra PrimaryColor
    {
        get => _bgra;
        set => SetField(ref _bgra, value);
    }

    private Bgra _secondaryBrush;
    public Bgra SecondaryColor
    {
        get => _secondaryBrush;
        set => SetField(ref _secondaryBrush, value);
	}

	private int _thickness;
    public int Thickness
    {
        get => _thickness;
        set => SetField(ref _thickness, value);
    }
}
