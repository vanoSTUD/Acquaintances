using Acquaintances.Bot.Application.Helpers;
using Acquaintances.Bot.Application.Services.EntityServices;
using Acquaintances.Bot.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot;
using Acquaintances.Bot.Application.Extensions;

namespace Acquaintances.Bot.Application.Services.UserStateHandlers;

public class SavePhotosHandler : StateHandlerBase
{
	private readonly ITelegramBotClient _bot;
	private readonly IServiceScopeFactory _scopeFactory;
	public SavePhotosHandler(ITelegramBotClient botClient, IServiceScopeFactory scopeFactory)
	{
		_bot = botClient;
		_scopeFactory = scopeFactory;
	}

	public override UserStates State => UserStates.SavePhotos;
	public override string CallbackData => CallbackQueryData.SavePhotos;

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

		if (user.Profile == null)
		{
			await BotMessagesHelper.SendErrorMessageAsync(_bot, chatId, ct);
			return;
		}

		var tempProfile = user.TempProfile;

		if (tempProfile == null || tempProfile.Photos == null || tempProfile.Photos.Count == 0)
		{
			await BotMessagesHelper.SendErrorMessageAsync(_bot, chatId, ct);
			return;
		}

		user.Profile.SetPhotos(tempProfile.Photos);

		await BotMessagesHelper.SendProfileAsync(_bot, chatId, user.Profile, ct);
		await userService.ClearTempProfileAsync(user, ct);
	}
}
