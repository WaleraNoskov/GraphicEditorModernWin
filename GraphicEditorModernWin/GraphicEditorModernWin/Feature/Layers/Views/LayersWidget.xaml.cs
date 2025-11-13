using GraphicEditorModernWin.Feature.Layers.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GraphicEditorModernWin.Feature.Layers.Views
{
    public sealed partial class LayersWidget : UserControl
    {
        public LayersWidget()
        {
            InitializeComponent();
            DataContext = App.AppHost.Services.GetRequiredService<LayersWidgetViewModel>();
		}
    }
}
