using GraphicEditorModernWin.Core.Contracts;

namespace GraphicEditorModernWin.Core.ValueTypes;

public record struct HistoryEntry(ICommand Command, CommandResult CommandResult, bool IsUndoed);
