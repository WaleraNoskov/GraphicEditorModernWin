using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicEditorModernWin.Core.Entities;

namespace GraphicEditorModernWin.Feature.Layers.LayersWidget;

internal class LayerViewModel(Layer value) 
{
	public Layer Value { get; } = value;
};