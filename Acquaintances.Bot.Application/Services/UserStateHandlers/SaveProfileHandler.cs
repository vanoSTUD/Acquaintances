using Acquaintances.Bot.Application.Extensions;
using Acquaintances.Bot.Application.Helpers;
using Acquaintances.Bot.Application.Services.EntityServices;
using Acquaintances.Bot.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Acquaintances.Bot.Application.Services.UserStateHandlers;

public class SaveProfileHandler : StateHandlerBase
{
	private readonly ITelegramBotClient _bot;
	private readonly IServiceScopeFactory _scopeFactory;
	public SaveProfileHandler(ITelegramBotClient botClient, IServiceScopeFactory scopeFactory)
	{
		_bot = botClient;
		_scopeFactory = scopeFactory;
	}

	public override UserStates State => UserStates.SaveProfile;
	public override string CallbackData => CallbackQueryData.SaveProfile;

	public override async Task Handle(Update update, CancellationToken ct = default)
	{
		if (update.CallbackQuery is not { } query)
		{
			return;
		}

		var chatId = update.GetChatId();
		using var scope = _scopeFactory.CreateScope();
		var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
		var user = await userService.GetOrCreateAsync(chatId, ct);

		await userService.SetStateAndUpdateAsync(user, UserStates.None, ct);

		var tempProfile = user.TempProfile;

		if (tempProfile == null)
		{
			await _bot.SendMessageHtml(chatId, $"Не нашел твои данные! Попробуй {CommandNames.Start}.", cancellationToken: ct);
			return;
		}

		var result = await userService.AddProfileAsync(user, tempProfile, ct);

		if (result.IsFailure)
		{
			await _bot.SendMessageHtml(chatId, result.Error, cancellationToken: ct);
			return;
		}

		await BotMessagesHelper.SendProfileAsync(_bot, chatId, user.Profile, ct);
		await userService.ClearTempProfile(user, ct);
	}
}
