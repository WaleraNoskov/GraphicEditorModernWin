using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using GraphicEditorModernWin.Core.Contracts;
using GraphicEditorModernWin.Core.ValueTypes;

namespace GraphicEditorModernWin.Core.Services;

public interface ICommandManager
{
	Result<CommandResult> Invoke<TCommand>(TCommand command) where TCommand : ICommand;
	Result Undo();
}
