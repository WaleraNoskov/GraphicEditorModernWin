using System;
using System.Windows.Input;

namespace GraphicEditorModernWin.Feature.Shared.Framework.Commands.Base;

internal abstract class BaseCommand : ICommand, IDisposable
{
	private EventHandler? _localHandlers;

	protected BaseCommand()
	{
		// Подписываемся на глобальное событие CommandManager, как в WPF
		CommandManager.RequerySuggested += OnRequerySuggested;
	}

	private void OnRequerySuggested(object? s, EventArgs e)
	{
		// Проксируем глобальное событие всем подписчикам
		_localHandlers?.Invoke(this, EventArgs.Empty);
	}

	public abstract bool CanExecute(object? parameter);
	public abstract void Execute(object? parameter);

	public event EventHandler? CanExecuteChanged
	{
		add => _localHandlers += value;
		remove => _localHandlers -= value;
	}

	/// <summary>
	/// Явно нотифицировать, что состояние CanExecute изменилось
	/// (обычно вызывается владельцем команды).
	/// </summary>
	public void RaiseCanExecuteChanged() => _localHandlers?.Invoke(this, EventArgs.Empty);

	#region IDisposable
	private bool _disposed = false;
	public virtual void Dispose()
	{
		if (_disposed) return;
		CommandManager.RequerySuggested -= OnRequerySuggested;
		_localHandlers = null;
		_disposed = true;
		GC.SuppressFinalize(this);
	}

	~BaseCommand() => Dispose();
	#endregion
}
