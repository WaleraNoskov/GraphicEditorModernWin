using System.Threading.Tasks;
using System.Windows.Input;

namespace GraphicEditorModernWin.Feature.Shared.Framework.Commands.Base;

/// <summary>
/// Represents the asynchronously <see cref="ICommand"/>.
/// </summary>
internal interface IAsyncCommand : ICommand
{
    /// <summary>
    /// Triggers command execution asynchronously.
    /// </summary>
    /// <param name="parameter">Any parameter for execution logic.</param>
    Task ExecuteAsync(object parameter);
}