using GraphicEditorModernWin.Core.Contracts;
using GraphicEditorModernWin.Core.Services;
using GraphicEditorModernWin.Core.ValueTypes;

namespace GraphicEditorModernWin.StandartPack.Services;

public class HistoryService : IHistoryService
{
    private readonly Stack<HistoryEntry> _history = new();

    public event EventHandler? HistoryChanged;

    public IReadOnlyCollection<HistoryEntry> History => _history;

    public void Push(ICommand command, CommandResult commandResult)
    {
        var newEntry = new HistoryEntry(command, commandResult, false);
        _history.Push(newEntry);

        HistoryChanged?.Invoke(this, EventArgs.Empty);
	}

    public HistoryEntry? Pop()
    {
        var gotEntry = _history.TryPop(out var entry);

        if(gotEntry)
            HistoryChanged?.Invoke(this, EventArgs.Empty);

        return gotEntry ? entry : null;
	}
}
