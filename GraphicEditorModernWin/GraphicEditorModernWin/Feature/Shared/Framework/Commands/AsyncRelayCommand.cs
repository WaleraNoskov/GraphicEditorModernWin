using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GraphicEditorModernWin.Feature.Shared.Framework.Commands.Base;

namespace GraphicEditorModernWin.Feature.Shared.Framework.Commands;

/// <summary>
/// Represents asynchronous MVVM relay command implementation.
/// </summary>
internal class AsyncRelayCommand : BaseCommand, IAsyncCommand
{
    private readonly Func<object?, Task> _executeAsync;
    private readonly Func<object?, bool>? _canExecute;
    private bool _isExecuting;

    public AsyncRelayCommand(Func<Task> executeAsync, Func<bool>? canExecute = null)
        : this(_ => executeAsync(), canExecute is null ? (Func<object?, bool>?)null : (_ => canExecute()))
    { }

    public AsyncRelayCommand(Func<object?, Task> executeAsync, Func<object?, bool>? canExecute = null)
    {
        _executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
        _canExecute = canExecute;
    }

    public override bool CanExecute(object? parameter) => !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);

    public override async void Execute(object? parameter) => await ExecuteAsync(parameter);

    public async Task ExecuteAsync(object? parameter)
    {
        if (_isExecuting) return;
        try
        {
            _isExecuting = true;
            RaiseCanExecuteChanged();
            await _executeAsync(parameter);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[AsyncRelayCommand] {ex}");
            throw;
        }
        finally
        {
            _isExecuting = false;
            RaiseCanExecuteChanged();
        }
    }
}

internal class AsyncRelayCommand<T> : BaseCommand, IAsyncCommand
{
    private readonly Func<T?, Task> _executeAsync;
    private readonly Func<T?, bool>? _canExecute;
    private bool _isExecuting;

    public AsyncRelayCommand(Func<T?, Task> executeAsync, Func<T?, bool>? canExecute = null)
    {
        _executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
        _canExecute = canExecute;
    }

    public override bool CanExecute(object? parameter)
    {
        if (_isExecuting) return false;
        return parameter is T t ? (_canExecute?.Invoke(t) ?? true) : (_canExecute?.Invoke(default) ?? true);
    }

    public override async void Execute(object? parameter) => await ExecuteAsync(parameter);

    public async Task ExecuteAsync(object? parameter)
    {
        if (_isExecuting) return;
        try
        {
            _isExecuting = true;
            RaiseCanExecuteChanged();
            if (parameter is T t)
                await _executeAsync(t);
            else
                await _executeAsync(default);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[AsyncRelayCommand<{typeof(T).Name}>] {ex}");
            throw;
        }
        finally
        {
            _isExecuting = false;
            RaiseCanExecuteChanged();
        }
    }
}