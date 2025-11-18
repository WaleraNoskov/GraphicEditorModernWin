using CSharpFunctionalExtensions;
using GraphicEditorModernWin.Core.Contracts;
using GraphicEditorModernWin.Core.Services;
using GraphicEditorModernWin.Core.ValueTypes;
using GraphicEditorModernWin.StandartPack.Command;
using GraphicEditorModernWin.Utils;
using OpenCvSharp;

namespace GraphicEditorModernWin.StandartPack.CommandHandlers;

public class StrokeCommandHandler(ILayersService layersService) : ICommandHandler<StrokeCommand>
{
    public Result<CommandResult> Execute(StrokeCommand command)
    {
        var layer = layersService.GetLayerById(command.LayerId);

        if(layer is null)
            return Result.Failure<CommandResult>($"Layer with id {command.LayerId} not found");
		if (command.Positions.Count < 2)
			return Result.Failure<CommandResult>("Not enough points to draw stroke");

		var mat = layer.Drawing;
		var points = command.Positions;
		var color = command.Color; // Scalar(B, G, R, A)
		var thickness = command.Thickness;

		var region = GetBoundingRect(points, thickness);
		var before = new Mat(mat, region).Clone();

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

	private static Rect GetBoundingRect(ICollection<Position> points, int thickness)
	{
		var minX = points.Min(p => p.X) - thickness;
		var minY = points.Min(p => p.Y) - thickness;
		var maxX = points.Max(p => p.X) + thickness;
		var maxY = points.Max(p => p.Y) + thickness;

		return new Rect(
			X: Math.Max(minX, 0),
			Y: Math.Max(minY, 0),
			Width: maxX - minX + 1,
			Height: maxY - minY + 1
		);
	}
}
