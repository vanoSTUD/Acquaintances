using Acquaintances.Bot.Application.Helpers;
using Acquaintances.Bot.Domain.Entities;
using Acquaintances.Bot.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot;
using Acquaintances.Bot.Application.Extensions;
using Telegram.Bot.Types.ReplyMarkups;
using Acquaintances.Bot.Application.Services.EntityServices;

namespace Acquaintances.Bot.Application.Services.UserStateHandlers;

public class SendingPhotoHandler : StateHandlerBase
{
	private readonly ITelegramBotClient _bot;
	private readonly IServiceScopeFactory _scopeFactory;
	public SendingPhotoHandler(ITelegramBotClient botClient, IServiceScopeFactory scopeFactory)
	{
		_bot = botClient;
		_scopeFactory = scopeFactory;
	}

	public override State State => State.SendingPhotos;

	public override async Task Execute(Update update, CancellationToken ct = default)
	{
		if (update.Message is not { } message)
		{
			return;
		}
		if (message.Photo is not { } inputPhotos)
		{
			await _bot.SendMessageHtml(update.Message.Chat, "Твоя цель - просто загрузить фотографию.", cancellationToken: ct);
			return;
		}

		var chatId = update.GetChatId();
		var inputPhoto = inputPhotos.Last();
		var photoResult = Photo.Create(inputPhoto.FileId, chatId);

		if (photoResult.IsFailure)
		{
			await _bot.SendMessageHtml(update.Message.Chat, photoResult.Error, cancellationToken: ct);
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

		tempProfile.Photos ??= [];
		tempProfile.Photos.Add(photoResult.Value);
		var photoCount = tempProfile.Photos.Count;

		await userService.SetTempProfileAsync(user, tempProfile, ct);

		if (photoCount >= Profile.MaxPhotos)
		{
			var result = await userService.AddProfileAsync(user, tempProfile);
			await userService.SetStateAsync(user, State.None, ct);

			if (result.IsFailure)
			{
				await _bot.SendMessageHtml(chatId, result.Error, cancellationToken: ct);
				return;
			}

			await BotMessagesHelper.SendProfile(_bot, user);
			await BotMessagesHelper.SendProfileCommands(_bot, chatId);
			return;
		}

		var keyboard = new InlineKeyboardMarkup().AddButton("Оставить", CallbackQueryData.SaveProfile); 
		var responceMessage = $"Загружено ({photoCount}/{Profile.MaxPhotos}) \nТы можешь добавить еще {Profile.MaxPhotos - photoCount} или оставить {(photoCount > 1 ? "эти" : "эту")}.";
		await _bot.SendMessageHtml(chatId, responceMessage, keyboard, cancellationToken: ct);
	}
}


