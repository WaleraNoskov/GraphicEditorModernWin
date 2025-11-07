using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicEditorModernWin.Core.ValueTypes;

namespace GraphicEditorModernWin.Feature.ColorPalete.Presentation.ColorPaletteWidget;

internal class BgraViewModel
{
	public Bgra Value { get; }

	public BgraViewModel(Bgra value) => Value = value;
	
}
