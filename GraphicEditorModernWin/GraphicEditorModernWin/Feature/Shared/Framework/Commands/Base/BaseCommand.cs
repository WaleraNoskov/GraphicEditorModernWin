using System;
using System.Windows.Input;

namespace GraphicEditorModernWin.Feature.Shared.Framework.Commands.Base;

internal abstract class BaseCommand : ICommand
{
    public abstract bool CanExecute(object? parameter);

    public abstract void Execute(object? parameter);

    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	

	public event EventHandler? CanExecuteChanged;
}