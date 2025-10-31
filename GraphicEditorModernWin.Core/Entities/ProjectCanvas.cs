using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicEditorModernWin.Core.ValueTypes;

namespace GraphicEditorModernWin.Core.Entities;

public class ProjectCanvas
{
	public Size Size { get; set; }
	public List<Layer> Layers { get; set; } = new();
}
