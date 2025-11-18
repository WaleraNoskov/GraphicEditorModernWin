using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicEditorModernWin.Feature.Shared.Framework;

namespace GraphicEditorModernWin.Feature.History.ViewModel;

class HistoryEntryViewModel : NotifyPropertyChangedBase
{
	private string _name;
	public string Name
	{
		get => _name;
		private set => SetField(ref _name, value);
	}

	private bool _isUndoed;
	public bool IsUndoed
	{
		get => _isUndoed;
		private set => SetField(ref _isUndoed, value);
	}

    public HistoryEntryViewModel(string name, bool isUndoed)
    {
        Name = name;
		IsUndoed = isUndoed;
    }
}
