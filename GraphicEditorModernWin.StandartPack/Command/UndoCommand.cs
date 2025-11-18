using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicEditorModernWin.Core.Contracts;
using GraphicEditorModernWin.Core.ValueTypes;

namespace GraphicEditorModernWin.StandartPack.Command;

internal record UndoCommand(CommandResult CommandResult) : ICommand;
