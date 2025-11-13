using GraphicEditorModernWin.Core.Contracts;
using GraphicEditorModernWin.Core.ValueTypes;

namespace GraphicEditorModernWin.Core.Services;

public interface IHistoryService
{
	IReadOnlyCollection<HistoryEntry> History { get; }
	void Push(ICommand command, CommandResult commandResult);
	HistoryEntry? Pop();
}
