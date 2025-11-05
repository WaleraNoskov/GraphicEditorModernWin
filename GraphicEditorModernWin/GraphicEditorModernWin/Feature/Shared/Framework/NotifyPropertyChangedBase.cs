using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEditorModernWin.Feature.Shared.Framework;

internal class NotifyPropertyChangedBase : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
	{
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}

	#region Disposing

	protected bool _disposed;

	/// <summary>
	/// Disposes object and unbinds <see cref="PropertyChanged"/> listeners.
	/// </summary>
	public virtual void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// <inheritdoc cref="Dispose"/>
	/// </summary>
	/// <param name="disposing">Need to free resources.</param>
	protected virtual void Dispose(bool disposing)
	{
		if (_disposed)
			return;

		if (disposing)
		{
			PropertyChanged = null;
		}

		_disposed = true;
	}

	~NotifyPropertyChangedBase()
	{
		Dispose(false);
	}

	#endregion
}
