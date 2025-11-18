using GraphicEditorModernWin.Core.ValueTypes;
using OpenCvSharp;

namespace GraphicEditorModernWin.Utils;

public static class BusinessToOpenCvSharpTools
{
	public static Point ToPoint(this Position position) => new(position.X, position.Y);

	public static Scalar ToScalar(this Bgra color) => new(color.B, color.G, color.R, color.A);

	public static Rectangle ToRectangle(this Rect rect) => new(new Position(rect.Left, rect.Top), new Core.ValueTypes.Size(rect.Width, rect.Height));
	public static Rect ToRect(this Rectangle rectangle) => new(rectangle.Position.X, rectangle.Position.Y, rectangle.Size.Width, rectangle.Size.Height);

	public static OpenCvSharp.Size ToSize(this Core.ValueTypes.Size size) => new(size.Width, size.Height);
	public static Core.ValueTypes.Size ToCoreSize(this OpenCvSharp.Size size) => new(size.Width, size.Height);
}
