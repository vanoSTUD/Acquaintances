using Acquaintances.Bot.Application.Extensions;
using Acquaintances.Bot.Application.Helpers;
using Acquaintances.Bot.Application.Services.EntityServices;
using Acquaintances.Bot.Domain.Entities;
using Acquaintances.Bot.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Acquaintances.Bot.Application.Services.CommandsHandlers;

public class StartCommandHandler : CommandHandlerBase
{
    private readonly IServiceScopeFactory _scopeFactory;

	public StartCommandHandler(IServiceScopeFactory scopeFactory)
	{
		_scopeFactory = scopeFactory;
	}

	public override string CommandName => CommandNames.Start;

    public override async Task Execute(Update update, AppUser user, ITelegramBotClient bot, CancellationToken ct = default)
    {
		using var scope = _scopeFactory.CreateScope();
		var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
		await userService.SetStateAsync(user, State.None, ct);

		var chatId = user.ChatId;
        var keyboard = new InlineKeyboardMarkup();

        if (user.Profile == null)
        {
            keyboard.AddButton(InlineKeyboardButton.WithCallbackData("Создать", CallbackQueryData.CreateProfile));
            await bot.SendMessageHtml(chatId, "У тебя еще нет профиля, создадим?", keyboard, ct);
            return;
        }

        await BotMessagesHelper.SendProfile(bot, chatId, user.Profile);
		await BotMessagesHelper.SendProfileCommands(bot, chatId);
	}
}
