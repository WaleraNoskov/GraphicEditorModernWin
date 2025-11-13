using System;
using GraphicEditorModernWin.Core.ValueTypes;
using Microsoft.UI.Xaml.Data;

namespace GraphicEditorModernWin.Feature.ColorPalete.Converters;

internal class BgraToWinColorColverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is Bgra bgra)
            return new Windows.UI.Color()
            {
                R = bgra.R,
                G = bgra.G,
                B = bgra.B,
                A = bgra.A
            };
        else if (value is Windows.UI.Color color)
            return new Bgra(color.B, color.G, color.R, color.A);
        else
            return null;
	}

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
	}
}
