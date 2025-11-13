using CSharpFunctionalExtensions;
using GraphicEditorModernWin.Core.ValueTypes;

namespace GraphicEditorModernWin.Core.Contracts;

public interface ICommandHandler<T> where T : ICommand
{
	public Result<CommandResult> Execute(T command);
}
