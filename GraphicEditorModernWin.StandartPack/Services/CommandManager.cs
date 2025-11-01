using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using GraphicEditorModernWin.Core.Contracts;
using GraphicEditorModernWin.Core.Services;
using GraphicEditorModernWin.Core.ValueTypes;
using GraphicEditorModernWin.StandartPack.Utils;
using Microsoft.Extensions.DependencyInjection;
using OpenCvSharp;

namespace GraphicEditorModernWin.StandartPack.Services;

internal class CommandManager(IServiceProvider serviceProvider, IHistoryService historyService, ILayersService layersService) : ICommandManager
{
    public Result<CommandResult> Invoke<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        if (handler is null)
            return Result.Failure<CommandResult>($"Handler for command {typeof(TCommand).Name} not found");

        return handler.Execute(command);
    }

    public Result Undo()
    {
        var entry = historyService.Pop();

        return entry.HasValue
            ? ResetFrame(entry.Value.CommandResult)
            : Result.Failure("No history entry to undo");

    }

    private Result ResetFrame(CommandResult commandResult)
    {
        var layer = layersService.GetLayerById(commandResult.LayerId);
        if (layer is null)
            return Result.Failure($"Layer with id {commandResult.LayerId} not found");

        if (commandResult.Region.Position.X < 0
            || commandResult.Region.Position.Y < 0
            || commandResult.Region.Position.X + commandResult.Region.Size.Width > layer.Drawing.Width
            || commandResult.Region.Position.Y + commandResult.Region.Size.Height > layer.Drawing.Height)
            return Result.Failure("Region is out of layer bounds");

        var roi = new Mat(layer.Drawing, commandResult.Region.ToRect());

        commandResult.Before.CopyTo(roi);

		return Result.Success();
	}
}
