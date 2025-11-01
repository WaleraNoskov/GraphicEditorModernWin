using GraphicEditorModernWin.Core.Services;
using GraphicEditorModernWin.StandartPack.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace GraphicEditorModernWin.StandartPack;

public static class DependencyInjection
{
	public static IServiceCollection AddStandartPackServices(this IServiceCollection services)
	{
		services
			.AddSingleton<ILayersService, ILayersService>()
			.AddSingleton<IHistoryService, IHistoryService>();

		return services;
	}
}
