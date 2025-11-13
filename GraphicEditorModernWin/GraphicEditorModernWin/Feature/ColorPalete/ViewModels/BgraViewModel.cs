using GraphicEditorModernWin.Core.ValueTypes;

namespace GraphicEditorModernWin.Feature.ColorPalete.ViewModels;

internal class BgraViewModel(Bgra value)
{
	public Bgra Value { get; } = value;
}
