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
using GraphicEditorModernWin.Utils;
using OpenCvSharp;

namespace GraphicEditorModernWin.StandartPack.CommandHandlers;

internal class UndoCommandHandler(ILayersService layersService) : ICommandHandler<UndoCommand>
{
    public Result<CommandResult> Execute(UndoCommand command)
    {
		var layer = layersService.GetLayerById(command.CommandResult.LayerId);
		if (layer is null)
			return Result.Failure<CommandResult>($"Layer with id {command.CommandResult.LayerId} not found");

		if (command.CommandResult.Region.Position.X < 0
			|| command.CommandResult.Region.Position.Y < 0
			|| command.CommandResult.Region.Position.X + command.CommandResult.Region.Size.Width > layer.Drawing.Width
			|| command.CommandResult.Region.Position.Y + command.CommandResult.Region.Size.Height > layer.Drawing.Height)
			return Result.Failure<CommandResult>("Region is out of layer bounds");

		var roi = new Mat(layer.Drawing, command.CommandResult.Region.ToRect());
		command.CommandResult.Before.CopyTo(roi);

		layersService.Edit(layer);
		return Result.Success(command.CommandResult);
	}
}
