using CSharpFunctionalExtensions;
using GraphicEditorModernWin.Core.Contracts;
using GraphicEditorModernWin.Core.ValueTypes;

namespace GraphicEditorModernWin.Core.Services;

public interface ICommandManager
{
	Result<CommandResult> Invoke<TCommand>(TCommand command) where TCommand : ICommand;
	Result Undo();
}
