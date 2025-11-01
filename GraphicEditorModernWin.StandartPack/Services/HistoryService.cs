using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicEditorModernWin.Core.Contracts;
using GraphicEditorModernWin.Core.Services;
using GraphicEditorModernWin.Core.ValueTypes;

namespace GraphicEditorModernWin.StandartPack.Services;

public class HistoryService : IHistoryService
{
    private readonly Stack<HistoryEntry> _history = new();
	public IReadOnlyCollection<HistoryEntry> History => _history;

    public void Push(ICommand command, CommandResult commandResult)
    {
        var newEntry = new HistoryEntry(command, commandResult, false);
        _history.Push(newEntry);
	}

    public HistoryEntry? Pop()
    {
        var gotEntry = _history.TryPop(out var entry);
        return entry;
	}
}
