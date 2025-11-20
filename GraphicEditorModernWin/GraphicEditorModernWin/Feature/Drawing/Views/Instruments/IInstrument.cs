using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GraphicEditorModernWin.Feature.Drawing.Contracts;
using Microsoft.Graphics.Canvas;
using Windows.UI;

namespace GraphicEditorModernWin.Feature.Drawing.Views.Instruments;

internal interface IInstrument
{
	void StartDrawing(Vector2 position);
	void MoveDrawing(Vector2 position);
	ICommitParameters EndDrawing();
	void Draw(CanvasDrawingSession session, Color color, float size);
}
