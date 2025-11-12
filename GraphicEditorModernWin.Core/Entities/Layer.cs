using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicEditorModernWin.Core.ValueTypes;
using OpenCvSharp;

namespace GraphicEditorModernWin.Core.Entities;

public class Layer
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public Mat Drawing { get; set; }
	public float Opacity { get; set; } = 1.0f;

    public Layer(ValueTypes.Size size)
	{
		Drawing = new Mat(new OpenCvSharp.Size(size.Width, size.Height), MatType.CV_8UC4);
	}
}
