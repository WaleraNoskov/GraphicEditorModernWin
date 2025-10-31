using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using GraphicEditorModernWin.Core.Contracts;
using GraphicEditorModernWin.Core.Services;
using GraphicEditorModernWin.Core.ValueTypes;
using GraphicEditorModernWin.StandartPack.Command;
using GraphicEditorModernWin.StandartPack.Utils;
using OpenCvSharp;

namespace GraphicEditorModernWin.StandartPack.CommandHandlers;

public class StrokeCommandHandler(ILayersService layersService) : ICommandHandler<StrokeCommand>
{
    public Result<CommandResult> Execute(StrokeCommand command)
    {
        var layer = layersService.GetLayerById(command.LayerId);

        if(layer is null)
            return Result.Failure<CommandResult>($"Layer with id {command.LayerId} not found");

		var mat = layer.Drawing;
		var points = command.Positions;
		var color = command.Color; // Scalar(B, G, R, A)
		var thickness = command.Thickness;

		if (points.Count < 2)
			return Result.Failure<CommandResult>("Not enough points to draw stroke");

		// --- Вычисляем ограничивающий прямоугольник для истории ---
		var region = GetBoundingRect(points, thickness);

		// --- Сохраняем исходный фрагмент для Undo ---
		var before = new Mat(mat, region).Clone();

		// --- Рисуем штрих ---
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

		// --- Сглаживание углов штриха (необязательно, но красиво) ---
		foreach (var point in points)
			Cv2.Circle(mat, point.ToPoint(), thickness, color.ToScalar(), -1, LineTypes.AntiAlias);

		// --- Сохраняем изменённый фрагмент ---
		var after = new Mat(mat, region).Clone();

		// --- Возвращаем результат ---
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
			Width: maxX - minX,
			Height: maxY - minY
		);
	}
}
