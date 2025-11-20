using System.Numerics;
using CSharpFunctionalExtensions;
using GraphicEditorModernWin.Core.ValueTypes;
using GraphicEditorModernWin.Feature.Drawing.Contracts;
using Microsoft.Graphics.Canvas;
using Windows.UI;

namespace GraphicEditorModernWin.Feature.Drawing.Views.Instruments;

internal class RectangleInstrument : IInstrument
{
    private Vector2? _startPosition;
    private Vector2? _endPosition;

    public void Draw(CanvasDrawingSession session, Color color, float size)
    {
        if (_startPosition.HasValue && _endPosition.HasValue)
            session.DrawRectangle(
                new Windows.Foundation.Rect(new Windows.Foundation.Point(_startPosition.Value.X, _startPosition.Value.Y), new Windows.Foundation.Point(_endPosition.Value.X, _endPosition.Value.Y)),
                color,
                size);
    }

    public Result<ICommitParameters> EndDrawing()
    {
        if (!_startPosition.HasValue || !_endPosition.HasValue)
            return Result.Failure<ICommitParameters>("No rectangle points");

        var position = new Position((int)_startPosition.Value.X, (int)_startPosition.Value.Y);
        var size = new Size((int)(_endPosition.Value.X - _startPosition.Value.X), (int)(_endPosition.Value.Y - _startPosition.Value.Y));

        var result = new RectangleCommitParameters(new Rectangle(position, size), true, true);

        _startPosition = null;
        _endPosition = null;

        return result;
    }

    public void MoveDrawing(Vector2 position)
    {
        _endPosition = position;
    }

    public void StartDrawing(Vector2 position)
    {
        _startPosition = position;
    }
}
