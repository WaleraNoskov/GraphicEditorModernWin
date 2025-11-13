using GraphicEditorModernWin.Core.Contracts;
using GraphicEditorModernWin.Core.Services;
using GraphicEditorModernWin.StandartPack.Command;
using GraphicEditorModernWin.StandartPack.CommandHandlers;
using GraphicEditorModernWin.StandartPack.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GraphicEditorModernWin.StandartPack;

public static class DependencyInjection
{
	public static IServiceCollection AddStandartPackServices(this IServiceCollection services)
	{
		services
			.AddSingleton<ILayersService, LayersService>()
			.AddSingleton<IHistoryService, HistoryService>()
			.AddSingleton<ICommandManager, CommandManager>()
			.AddSingleton<IColorPaletteService, ColorPaletteService>();

		services
			.AddTransient<ICommandHandler<StrokeCommand>, StrokeCommandHandler>();

		return services;
	}
}
