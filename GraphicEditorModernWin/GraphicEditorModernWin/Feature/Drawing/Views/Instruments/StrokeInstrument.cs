using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using CSharpFunctionalExtensions;
using GraphicEditorModernWin.Core.ValueTypes;
using GraphicEditorModernWin.Feature.Drawing.Contracts;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Windows.UI;

namespace GraphicEditorModernWin.Feature.Drawing.Views.Instruments;

internal class StrokeInstrument : IInstrument
{
    private readonly List<Vector2> _currentStroke = [];

    public void StartDrawing(Vector2 position)
    {
        _currentStroke.Add(position);
    }

    public void MoveDrawing(Vector2 position)
    {
        _currentStroke.Add(position);
    }

    public Result<ICommitParameters> EndDrawing()
    {
        if (_currentStroke.Count <= 0)
            return Result.Failure<ICommitParameters>("No stroke points.");

        var result = new StrokeCommitParameters([.. _currentStroke.Select(v => new Position((int)v.X, (int)v.Y))]);
        _currentStroke.Clear();

        return result;
    }

    public void Draw(CanvasDrawingSession session, Color color, float size)
    {
        if (_currentStroke.Count == 1)
            session.DrawCircle(_currentStroke[0], size, color);
        else
            for (int i = 1; i < _currentStroke.Count; i++)
                session.DrawLine(_currentStroke[i - 1], _currentStroke[i], color, size);
    }
}
