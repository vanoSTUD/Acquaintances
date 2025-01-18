using Acquaintances.Bot.Application.Extensions;
using Acquaintances.Bot.Application.Helpers;
using Acquaintances.Bot.Application.Services.EntityServices;
using Acquaintances.Bot.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Acquaintances.Bot.Application.Services.UserStateHandlers;

public class CreatingProfileHandler : StateHandlerBase
{
	private readonly ITelegramBotClient _bot;
	private readonly IServiceScopeFactory _scopeFactory;
	public CreatingProfileHandler(ITelegramBotClient botClient, IServiceScopeFactory scopeFactory)
	{
		_bot = botClient;
		_scopeFactory = scopeFactory;
	}

	public override UserStates State => UserStates.CreatingProfile;
	public override string CallbackData => CallbackQueryData.CreateProfile;

	public override async Task Handle(Update update, CancellationToken ct = default)
	{
		var chatId = update.GetChatId();

		using var scope = _scopeFactory.CreateScope();
		var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
		var user = await userService.GetOrCreateAsync(chatId, ct);

		await _bot.SendMessageHtml(chatId, "Создание анкеты. \nВведи своё имя:", cancellationToken: ct);

		await userService.SetStateAsync(user, UserStates.EnteringName, ct);
	}
}
