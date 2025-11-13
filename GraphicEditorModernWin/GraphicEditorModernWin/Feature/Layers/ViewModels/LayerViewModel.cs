using GraphicEditorModernWin.Core.Entities;

namespace GraphicEditorModernWin.Feature.Layers.ViewModels;

internal class LayerViewModel(Layer value) 
{
	public Layer Value { get; } = value;
};