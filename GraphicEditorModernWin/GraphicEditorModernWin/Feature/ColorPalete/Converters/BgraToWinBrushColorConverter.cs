using System;
using GraphicEditorModernWin.Core.ValueTypes;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace GraphicEditorModernWin.Feature.ColorPalete.Converters;

internal class BgraToWinBrushColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
		if (value is SolidColorBrush brush)
			return new Bgra(brush.Color.B, brush.Color.G, brush.Color.R, brush.Color.A);
		else if (value is Bgra bgra)
			return new SolidColorBrush(Color.FromArgb(bgra.A, bgra.R, bgra.G, bgra.B));
		else
			return null;
	}

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
