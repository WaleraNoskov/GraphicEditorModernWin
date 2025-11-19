using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Windows.Input;
using GraphicEditorModernWin.Feature.Shared.Framework.Commands;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GraphicEditorModernWin.Feature.Drawing.Views;

internal sealed partial class ZoomControl : UserControl
{
	public ZoomControl()
	{
		InitializeComponent();
	}

	public double Zoom
	{
		get => (double)GetValue(ZoomProperty);
		set => SetValue(ZoomProperty, value);
	}

	public static readonly DependencyProperty ZoomProperty =
		DependencyProperty.Register(
			nameof(Zoom),
			typeof(double),
			typeof(ZoomControl),
			new PropertyMetadata(1.0, OnZoomChanged));

	public event EventHandler<ZoomChangedEventArgs>? ZoomChanged;

	public RelayCommand<double> Command
	{
		get => (RelayCommand<double>)GetValue(CommandProperty);
		set => SetValue(CommandProperty, value);
	}

	public static readonly DependencyProperty CommandProperty =
		DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ZoomControl), new PropertyMetadata(null));

	private static void OnZoomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var control = (ZoomControl)d;
		double newZoom = (double)e.NewValue;

		control.ZoomTextBlock.Text = $"{Math.Round(newZoom * 100)} %";

		control.Command?.Execute(e.NewValue);
		control.ZoomChanged?.Invoke(
			control,
			new ZoomChangedEventArgs((double)e.OldValue, (double)e.NewValue)
		);
	}

	private void IncreaseZoom_Click(object sender, RoutedEventArgs e) => IncreaseZoom();
	private void DecreaseZoom_Click(object sender, RoutedEventArgs e) => DecreaseZoom();

	private void IncreaseZoom()
	{
		if(Zoom <= 4)
			Zoom += 0.1;
	}

	private void DecreaseZoom()
	{
		if(Zoom >= 0.2)
			Zoom -= 0.1;
	}
}
public class ZoomChangedEventArgs : EventArgs
{
	public double OldValue { get; }
	public double NewValue { get; }

	public ZoomChangedEventArgs(double oldVal, double newVal)
	{
		OldValue = oldVal;
		NewValue = newVal;
	}
}