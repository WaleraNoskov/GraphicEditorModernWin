using OpenCvSharp;

namespace GraphicEditorModernWin.Core.Entities;

public class Layer
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public Mat Drawing { get; set; }
	public float Opacity { get; set; } = 1.0f;
	public int Order { get; set; }

    public Layer(ValueTypes.Size size)
	{
		Drawing = new Mat(new Size(size.Width, size.Height), MatType.CV_8UC4);
	}
}
