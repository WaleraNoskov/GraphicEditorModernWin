using CSharpFunctionalExtensions;
using GraphicEditorModernWin.Core.Contracts;
using GraphicEditorModernWin.Core.Services;
using GraphicEditorModernWin.Core.ValueTypes;
using GraphicEditorModernWin.StandartPack.Command;
using GraphicEditorModernWin.Utils;
using Microsoft.Extensions.DependencyInjection;
using OpenCvSharp;

namespace GraphicEditorModernWin.StandartPack.Services;

internal class CommandManager(IServiceProvider serviceProvider, IHistoryService historyService, ICommandHandler<UndoCommand> undoCommandHandler) : ICommandManager
{
	public Result<CommandResult> Invoke<TCommand>(TCommand command) where TCommand : ICommand
	{
		var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
		if (handler is null)
			return Result.Failure<CommandResult>($"Handler for command {typeof(TCommand).Name} not found");

		var result = handler.Execute(command);

		if (result.IsSuccess)
			historyService.Push(command, result.Value);

		return result;
	}

	public Result Undo()
	{
		var entry = historyService.Pop();
		if (entry is null)
			return Result.Failure("No history entries for undo");

		var result = undoCommandHandler.Execute(new UndoCommand(entry.Value.CommandResult));

		if (result.IsFailure)
			historyService.Push(entry.Value.Command, entry.Value.CommandResult);

		return result;
	}
}
