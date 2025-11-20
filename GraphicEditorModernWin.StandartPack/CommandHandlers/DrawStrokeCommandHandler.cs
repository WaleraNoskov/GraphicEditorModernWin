using CSharpFunctionalExtensions;
using GraphicEditorModernWin.Core.Contracts;
using GraphicEditorModernWin.Core.Services;
using GraphicEditorModernWin.Core.ValueTypes;
using GraphicEditorModernWin.StandartPack.Command;
using GraphicEditorModernWin.Utils;
using OpenCvSharp;

namespace GraphicEditorModernWin.StandartPack.CommandHandlers;

public class DrawStrokeCommandHandler(ILayersService layersService) : ICommandHandler<DrawStrokeCommand>
{
    public Result<CommandResult> Execute(DrawStrokeCommand command)
    {
        var layer = layersService.GetLayerById(command.LayerId);

        if (layer is null)
            return Result.Failure<CommandResult>($"Layer with id {command.LayerId} not found");
        if (command.Positions.Count < 1)
            return Result.Failure<CommandResult>("Not enough points to draw stroke");

        var mat = layer.Drawing;
        var points = command.Positions;
        var color = command.Color; // Scalar(B, G, R, A)
        var thickness = command.Thickness;

        var region = GetBoundingRect(points, thickness, new Core.ValueTypes.Size(mat.Width, mat.Height));
        var before = new Mat(mat, region).Clone();

        if (points.Count == 1)
            Cv2.Circle(mat, points[0].ToPoint(), thickness, color.ToScalar(), -1, LineTypes.AntiAlias);

        else
            for (int i = 1; i < points.Count; i++)
            {
                Cv2.Line(
                    img: mat,
                    pt1: points[i - 1].ToPoint(),
                    pt2: points[i].ToPoint(),
                    color: color.ToScalar(),
                    thickness: thickness,
                    lineType: LineTypes.AntiAlias
                );
            }

        layersService.Edit(layer);
        return Result.Success(new CommandResult(command.LayerId, region.ToRectangle(), before));
    }

    private static Rect GetBoundingRect(ICollection<Position> points, int thickness, Core.ValueTypes.Size imageSize)
    {
        var minX = points.Min(p => p.X) - thickness - 1;
        var minY = points.Min(p => p.Y) - thickness - 1;
        var maxX = points.Max(p => p.X) + thickness + 3;
        var maxY = points.Max(p => p.Y) + thickness + 3;

        const int antialiasReserve = 2;

        var x = Math.Max(0, points.Min(p => p.X) - thickness - antialiasReserve);
        var y = Math.Max(0, points.Min(p => p.Y) - thickness - antialiasReserve);
        var width = Math.Min(imageSize.Width - x, points.Max(p => p.X) + thickness + antialiasReserve * 2 - x);
        var height = Math.Min(imageSize.Height - y, points.Max(p => p.Y) + thickness + antialiasReserve * 2 - y);

        return new Rect(x, y, width, height);
    }
}
