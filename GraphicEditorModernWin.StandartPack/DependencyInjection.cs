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
			.AddSingleton<ILayersService, ILayersService>()
			.AddSingleton<IHistoryService, IHistoryService>()
			.AddSingleton<ICommandManager, CommandManager>();

		services
			.AddTransient<ICommandHandler<StrokeCommand>, StrokeCommandHandler>();

		return services;
	}
}
