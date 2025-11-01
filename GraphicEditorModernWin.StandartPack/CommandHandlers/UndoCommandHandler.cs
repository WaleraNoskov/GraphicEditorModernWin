using CSharpFunctionalExtensions;
using GraphicEditorModernWin.Core.Contracts;
using GraphicEditorModernWin.Core.ValueTypes;
using GraphicEditorModernWin.StandartPack.Command;

namespace GraphicEditorModernWin.StandartPack.CommandHandlers;

internal class UndoCommandHandler : ICommandHandler<UndoCommand>
{
    public Result<CommandResult> Execute(UndoCommand command)
    {
        throw new NotImplementedException();
    }
}
