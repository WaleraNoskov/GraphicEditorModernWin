using GraphicEditorModernWin.Feature.Drawing.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GraphicEditorModernWin.Feature.Drawing.Views;

internal sealed partial class RenderWidget : UserControl
{
    private RenderViewModel _viewModel => (DataContext as RenderViewModel)!;

    public RenderWidget()
    {
        InitializeComponent();

        DataContext = App.AppHost.Services.GetRequiredService<RenderViewModel>();
        _viewModel.PropertyChanged += _viewModel_PropertyChanged;

        CanvasScrollView.ZoomTo(0.5f, null);
    }

    private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(_viewModel.Layers))
        {
            LayersContainer.Children.Clear();

            foreach (var vm in _viewModel.Layers)
                LayersContainer.Children.Add(new RenderLayerControl() { DataContext = vm });
        }
    }
}
