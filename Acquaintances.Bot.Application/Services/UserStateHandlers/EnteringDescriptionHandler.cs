using Acquaintances.Bot.Application.Helpers;
using Acquaintances.Bot.Domain.Entities;
using Acquaintances.Bot.Domain.Enums;
using Acquaintances.Bot.Domain.ValueObjects.Profile;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot;
using Acquaintances.Bot.Application.Extensions;
using Acquaintances.Bot.Application.Services.EntityServices;
using Telegram.Bot.Types.ReplyMarkups;

namespace Acquaintances.Bot.Application.Services.UserStateHandlers;

public class EnteringDescriptionHandler : StateHandlerBase
{
	private readonly ITelegramBotClient _bot;
	private readonly IServiceScopeFactory _scopeFactory;
	public EnteringDescriptionHandler(ITelegramBotClient botClient, IServiceScopeFactory scopeFactory)
	{
		_bot = botClient;
		_scopeFactory = scopeFactory;
	}

	public override State State => State.EnteringDescription;

	public override async Task Execute(Update update, CancellationToken ct = default)
	{
		if (update.Message is not { } message)
			return;

		var inputDescription = message.Text;
		var descriptionResult = Description.Create(inputDescription);
		var chatId = update.GetChatId();

		if (descriptionResult.IsFailure)
		{
			await _bot.SendMessageHtml(chatId, descriptionResult.Error, cancellationToken: ct);
			return;
		}

		using var scope = _scopeFactory.CreateScope();
		var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
		var user = await userService.GetOrCreateAsync(chatId, ct);
		var tempProfile = user.GetTempProfile();

		if (tempProfile == null)
		{
			await _bot.SendMessageHtml(chatId, $"Ошибка! Попробуй {CommandNames.Start}.", cancellationToken: ct);
			return;
		}

		var responceMessage = $"Принято. Теперь отправь фотографии до {Profile.MaxPhotos} шт, по одной на сообщение:";
		await _bot.SendMessageHtml(chatId, responceMessage, cancellationToken: ct);

		tempProfile.Description = descriptionResult.Value;

		await userService.SetTempProfileAsync(user, tempProfile, ct);
		await userService.SetStateAsync(user, State.SendingPhotos, ct);
	}
}

