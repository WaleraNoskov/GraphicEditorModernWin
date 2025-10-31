using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicEditorModernWin.Core.Contracts;

namespace GraphicEditorModernWin.Core.ValueTypes;

public record struct HistoryEntry(ICommand Command, CommandResult CommandResult, bool IsUndoed);
