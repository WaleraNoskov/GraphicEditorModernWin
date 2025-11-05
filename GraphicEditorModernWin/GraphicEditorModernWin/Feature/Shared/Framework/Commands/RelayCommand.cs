using System;
using GraphicEditorModernWin.Feature.Shared.Framework.Commands.Base;

namespace GraphicEditorModernWin.Feature.Shared.Framework.Commands;

/// <summary>
/// Represents MVVM relay command implementation.
/// </summary>
internal class RelayCommand : BaseCommand
{
    private readonly Action<object?> _execute;
    private readonly Func<object?, bool>? _canExecute;

    public RelayCommand(Action execute, Func<bool>? canExecute = null)
        : this(_ => execute(), canExecute is null ? (Func<object?, bool>?)null : (_ => canExecute()))
    { }

    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public override bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
    public override void Execute(object? parameter) => _execute(parameter);
}

internal class RelayCommand<T> : BaseCommand
{
    private readonly Action<T?> _execute;
    private readonly Func<T?, bool>? _canExecute;

    public RelayCommand(Action<T?> execute, Func<T?, bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public override bool CanExecute(object? parameter)
    {
        if (_canExecute == null) return true;
        return parameter is T t ? _canExecute(t) : _canExecute(default);
    }

    public override void Execute(object? parameter)
    {
        if (parameter is T t)
            _execute(t);
        else
            _execute(default);
    }
}