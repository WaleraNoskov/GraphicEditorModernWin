using GraphicEditorModernWin.Feature.ColorPalete.Busines;
using GraphicEditorModernWin.Feature.ColorPalete.Presentation.ColorPaletteWidget;
using GraphicEditorModernWin.Feature.MainWindow;
using GraphicEditorModernWin.Feature.Shared.Framework;
using GraphicEditorModernWin.Feature.Shared.Services;
using GraphicEditorModernWin.StandartPack;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

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


			_window = AppHost.Services.GetRequiredService<MainWindow>();
			_window.Activate();
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services
                .AddStandartPackServices();

            services
                .AddSingleton<BrushStateService>();

            services
                .AddTransient<ColorPaletteModel>()
                .AddTransient<ColorPaletteWidgetViewModel>()
                .AddTransient<ColorPaletteWidget>();

			services.AddTransient<MainWindow>();
		}
	}
}
