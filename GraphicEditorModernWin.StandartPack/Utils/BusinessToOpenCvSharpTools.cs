using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicEditorModernWin.Core.ValueTypes;
using OpenCvSharp;

namespace GraphicEditorModernWin.StandartPack.Utils;

internal static class BusinessToOpenCvSharpTools
{
	internal static Point ToPoint(this Position position) => new(position.X, position.Y);
    internal static Scalar ToScalar(this Bgra color) => new(color.B, color.G, color.R, color.A);
	internal static Rectangle ToRectangle(this Rect rect) => new(new Position(rect.Left, rect.Top), new Core.ValueTypes.Size(rect.Width, rect.Height));
}
