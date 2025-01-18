using Acquaintances.Bot.Application.Abstractions;
using Acquaintances.Bot.Application.Services;
using Acquaintances.Bot.Application.Services.CommandsHandlers;
using Acquaintances.Bot.Application.Services.EntityServices;
using Acquaintances.Bot.Application.Services.UserStateHandlers;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;

namespace Acquaintances.Bot.Application;

public static class DependencyInjection
{
	public static void AddApplicationServices(this IServiceCollection services)
	{
		services.AddSingleton<ExceptionHandler>();
		services.AddSingleton<IHandler<Update>, UpdateHandler>();
		services.AddSingleton<IHandler<Message>, MessageHandler>();
		services.AddSingleton<IHandler<CallbackQuery>, CallbackQueryHandler>();

		services.AddSingleton<StateHandlerBase, CreatingProfileHandler>();
		services.AddSingleton<StateHandlerBase, EnteringNameHandler>();
		services.AddSingleton<StateHandlerBase, EnteringAgeHandler>();
		services.AddSingleton<StateHandlerBase, EnteringCityHandler>();
		services.AddSingleton<StateHandlerBase, EnteringDescriptionHandler>();
		services.AddSingleton<StateHandlerBase, EnteringGenderHandler>();
		services.AddSingleton<StateHandlerBase, EnteringPreferedGenderHandler>();
		services.AddSingleton<StateHandlerBase, SaveProfileHandler>();
		services.AddSingleton<StateHandlerBase, SendingPhotoHandler>();
		services.AddSingleton<StateHandlerBase, ChangingDescriptionHandler>();

		services.AddSingleton<CommandHandlerBase, StartCommandHandler>();

		services.AddScoped<IUserService, UserService>();

	}
}
