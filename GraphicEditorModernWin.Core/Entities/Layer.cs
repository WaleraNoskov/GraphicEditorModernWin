using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace GraphicEditorModernWin.Core.Entities;

public class Layer
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public Mat Drawing { get; set; } = new();
	public float Opacity { get; set; } = 1.0f;
}
