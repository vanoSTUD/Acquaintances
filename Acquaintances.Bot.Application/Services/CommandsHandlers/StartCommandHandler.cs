using Acquaintances.Bot.Application.Extensions;
using Acquaintances.Bot.Application.Helpers;
using Acquaintances.Bot.Application.Services.EntityServices;
using Acquaintances.Bot.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Acquaintances.Bot.Application.Services.CommandsHandlers;

public class StartCommandHandler : CommandHandlerBase
{
	private readonly IServiceScopeFactory _scopeFactory;
	private readonly ITelegramBotClient _bot;

    public StartCommandHandler(IServiceScopeFactory scopeFactory, ITelegramBotClient bot)
    {
        _scopeFactory = scopeFactory;
        _bot = bot;
    }

    public override string CommandName => CommandNames.Start;

	public override async Task Handle(Update update, CancellationToken ct = default)
	{
		var chatId = update.GetChatId();
		using var scope = _scopeFactory.CreateScope();
		var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
		var user = await userService.GetOrCreateAsync(chatId, ct);
		await userService.SetStateAsync(user, UserStates.None, ct);

		var keyboard = new InlineKeyboardMarkup();

		if (user.Profile == null)
		{
			keyboard.AddButton("Создать", CallbackQueryData.CreateProfile);
			await _bot.SendMessageHtml(chatId, "У тебя еще нет профиля, создадим?", keyboard, ct);
			return;
		}

		await BotMessagesHelper.ShowProfile(_bot, chatId, user.Profile);
		await BotMessagesHelper.ShowProfileCommands(_bot, chatId);
	}
}
