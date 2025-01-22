using Acquaintances.Bot.Application.Helpers;
using Acquaintances.Bot.Application.Services.EntityServices;
using Acquaintances.Bot.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot;
using Acquaintances.Bot.Application.Extensions;
using Acquaintances.Bot.Domain.ValueObjects.Profile;
using Telegram.Bot.Types.ReplyMarkups;

namespace Acquaintances.Bot.Application.Services.UserStateHandlers;

/// <summary>
/// Обрабатывает сallbackQurey и само собщение для изменения описания анкеты
/// </summary>
public class ViewMyProfileHandler : StateHandlerBase
{
    private readonly ITelegramBotClient _bot;
    private readonly IServiceScopeFactory _scopeFactory;
    public ViewMyProfileHandler(ITelegramBotClient botClient, IServiceScopeFactory scopeFactory)
    {
        _bot = botClient;
        _scopeFactory = scopeFactory;
    }

    public override UserStates State => UserStates.ViewMyProfile;
    public override string CallbackData => CallbackQueryData.ViewMyProfile;

	public override async Task Handle(Update update, CancellationToken ct = default)
    {
        if (update.CallbackQuery == null && update.Message == null) 
        {
			//Todo: обработка нештатной ситуации
			return;
		}

		var chatId = update.GetChatId();
		using var scope = _scopeFactory.CreateScope();
		var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
		var user = await userService.GetOrCreateAsync(chatId, ct);

		if (user.Profile == null)
		{
			await BotMessagesHelper.SendNoProfileMessageAsync(_bot, chatId, ct);
			await userService.SetStateAndUpdateAsync(user, UserStates.None, ct);
			return;
		}

		if (update.CallbackQuery != null)
		{
			await BotMessagesHelper.SendProfileAsync(_bot, chatId, user.Profile, ct);
			await userService.SetStateAndUpdateAsync(user, UserStates.None, ct);
		}
	}
}

