using System.Collections.ObjectModel;
using System.Windows.Input;
using GraphicEditorModernWin.Core.Services;
using GraphicEditorModernWin.Feature.Shared.Framework;
using GraphicEditorModernWin.Feature.Shared.Framework.Commands;

namespace GraphicEditorModernWin.Feature.History.ViewModel;

class HistoryViewModel : NotifyPropertyChangedBase
{
	private readonly IHistoryService _historyService;
    private readonly ICommandManager _commandManager;

    public HistoryViewModel(IHistoryService historyService, ICommandManager commandManager)
    {
        _historyService = historyService;
        _historyService.HistoryChanged += (sender, args) => RestoreHistory();
        _commandManager = commandManager;

        InitializeCommand = new RelayCommand(OnInitializeCommandExecuted);
        UndoCommand = new RelayCommand(OnUndoCommandExecuted);
    }

    public ObservableCollection<HistoryEntryViewModel> HistoryEntries { get; } = [];

    public ICommand InitializeCommand { get; private set; }
    private void OnInitializeCommandExecuted() => RestoreHistory();

    public ICommand UndoCommand { get; private set; }
    private void OnUndoCommandExecuted() => _commandManager.Undo();

    private void RestoreHistory()
    {
        HistoryEntries.Clear();
        foreach (var entry in _historyService.History)
            HistoryEntries.Add(new HistoryEntryViewModel(nameof(entry.GetType), entry.IsUndoed));
    }
}
