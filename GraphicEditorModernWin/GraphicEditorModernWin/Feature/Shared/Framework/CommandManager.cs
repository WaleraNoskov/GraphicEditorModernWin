using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GraphicEditorModernWin.Feature.Shared.Framework;

internal class CommandManager
{
	private static SynchronizationContext? _uiContext = null;

	/// <summary>
	/// Call once from UI thread (e.g. App.OnLaunched) to capture UI context.
	/// If not called, fallback будет пытаться вызывать обработчики в текущем потоке.
	/// </summary>
	public static void InitializeForUiThread()
	{
		_uiContext = SynchronizationContext.Current;
	}

	/// <summary>
	/// Event that commands can subscribe to, similar to WPF's RequerySuggested.
	/// </summary>
	public static event EventHandler? RequerySuggested;

	/// <summary>
	/// Triggers re-evaluation of CanExecute for all subscribers.
	/// Should be called when application state changed and commands should update.
	/// Safe to call from any thread.
	/// </summary>
	public static void InvalidateRequerySuggested()
	{
		var handlers = RequerySuggested;
		if (handlers is null) return;

		// Post invocation to UI synchronization context if available
		if (_uiContext != null)
		{
			_uiContext.Post(_ => handlers.Invoke(null, EventArgs.Empty), null);
		}
		else
		{
			// best-effort fallback: invoke on current thread
			handlers.Invoke(null, EventArgs.Empty);
		}
	}
}
