using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GraphicEditorModernWin.Core.ValueTypes;

namespace GraphicEditorModernWin.Core.Services;

public interface IHistoryService
{
	public IReadOnlyCollection<HistoryEntry> History { get; }
	public void Push(ICommand Command, CommandResult commandResult);
	public void Undo();
}
