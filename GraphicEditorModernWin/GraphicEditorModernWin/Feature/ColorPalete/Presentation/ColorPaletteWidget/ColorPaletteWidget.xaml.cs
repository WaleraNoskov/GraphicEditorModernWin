using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GraphicEditorModernWin.Feature.ColorPalete.Presentation.ColorPaletteWidget;

public sealed partial class ColorPaletteWidget : UserControl
{
    public ColorPaletteWidget()
    {
        InitializeComponent();
        DataContext = App.AppHost.Services.GetRequiredService<ColorPaletteWidgetViewModel>();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        PrimaryColor.Flyout.Hide();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        PrimaryColor.Flyout.Hide();
    }
}
