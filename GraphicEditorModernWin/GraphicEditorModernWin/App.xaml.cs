using GraphicEditorModernWin.Core.Services;
using GraphicEditorModernWin.Feature.Shared.Framework;
using GraphicEditorModernWin.StandartPack;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using GraphicEditorModernWin.CustomWindows;
using GraphicEditorModernWin.Feature.Layers.ViewModels;
using GraphicEditorModernWin.Feature.ColorPalete.ViewModels;
using GraphicEditorModernWin.Feature.ColorPalete.Views;
using GraphicEditorModernWin.Feature.Layers.Views;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GraphicEditorModernWin
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private Window? _window;
        public static IHost AppHost { get; private set; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();

            AppHost = Host
                .CreateDefaultBuilder()
                .ConfigureServices(ConfigureServices)
                .Build();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            CommandManager.InitializeForUiThread();

            var layersService = AppHost.Services.GetRequiredService<ILayersService>();
            layersService.AddLayer(new Core.Entities.Layer(new Core.ValueTypes.Size(800, 600)));

			_window = AppHost.Services.GetRequiredService<MainWindow>();
			_window.Activate();
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services
                .AddStandartPackServices();

            services
                .AddTransient<ColorPaletteWidgetViewModel>()
                .AddTransient<ColorPaletteWidget>();

            services
                .AddTransient<LayersWidgetViewModel>()
                .AddTransient<LayersWidget>();

			services.AddTransient<MainWindow>();
		}
	}
}
