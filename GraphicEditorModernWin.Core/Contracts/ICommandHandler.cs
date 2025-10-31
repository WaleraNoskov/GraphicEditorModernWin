using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using GraphicEditorModernWin.Core.ValueTypes;

namespace GraphicEditorModernWin.Core.Contracts;

public interface ICommandHandler<T> where T : ICommand
{
	public Result<CommandResult> Execute(T command);
}
