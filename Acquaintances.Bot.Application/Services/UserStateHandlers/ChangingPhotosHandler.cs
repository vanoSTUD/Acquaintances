using Acquaintances.Bot.Application.Helpers;
using Acquaintances.Bot.Application.Services.EntityServices;
using Acquaintances.Bot.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot;
using Acquaintances.Bot.Application.Extensions;
using Acquaintances.Bot.Domain.ValueObjects.Profile;
using Telegram.Bot.Types.ReplyMarkups;
using Acquaintances.Bot.Domain.Models;
using Acquaintances.Bot.Domain.Entities;

namespace Acquaintances.Bot.Application.Services.UserStateHandlers;

/// <summary>
/// Обрабатывает сallbackQuery и само собщение для изменения фотографий анкеты
/// </summary>
public class ChangingPhotosHandler : StateHandlerBase
{
    private readonly ITelegramBotClient _bot;
    private readonly IServiceScopeFactory _scopeFactory;
    public ChangingPhotosHandler(ITelegramBotClient botClient, IServiceScopeFactory scopeFactory)
    {
        _bot = botClient;
        _scopeFactory = scopeFactory;
    }

    public override UserStates State => UserStates.ChangingPhotos;
    public override string CallbackData => CallbackQueryData.ChangingPhotos;

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
			if (user.TempProfile == null)
				user.SetTempProfile(new TempProfile());

			user.TempProfile!.Photos = [];

			var keyboard = BotMessagesHelper.GetStopChangesKeyboard();
			var responceMessage = $"Скидывай фотографии, по одной:";
			await _bot.SendMessageHtml(chatId, responceMessage, keyboard, cancellationToken: ct);

			await userService.SetStateAndUpdateAsync(user, UserStates.ChangingPhotos, ct);
			return;
		}

		if (update.Message != null)
		{
			if (update.Message.Photo is not { } inputPhotoSizes)
			{
				await _bot.SendMessageHtml(update.Message.Chat, "Твоя цель - просто загрузить фотографию.", cancellationToken: ct);
				return;
			}

			var inputPhoto = inputPhotoSizes.Last();
			var photoResult = Photo.Create(inputPhoto.FileId, chatId);

			if (photoResult.IsFailure)
			{
				await _bot.SendMessageHtml(chatId, photoResult.Error, cancellationToken: ct);
				return;
			}

			var tempProfile = user.TempProfile;

			if (tempProfile == null)
			{
				await BotMessagesHelper.SendErrorMessageAsync(_bot, chatId, ct);
				return;
			}

			tempProfile.Photos ??= [];
			tempProfile.Photos.Add(photoResult.Value);
			var photoCount = tempProfile.Photos.Count;

			await userService.SetTempProfileAsync(user, tempProfile, ct);

			if (photoCount >= Profile.MaxPhotos)
			{
				user.Profile.SetPhotos(tempProfile.Photos);

				await BotMessagesHelper.SendProfileAsync(_bot, chatId, user.Profile, ct);
				await userService.ClearTempProfileAsync(user, ct);
				return;
			}

			var keyboard = new InlineKeyboardMarkup().AddButton("Оставить", CallbackQueryData.SavePhotos);
			var responceMessage = $"Загружено ({photoCount}/{Profile.MaxPhotos}) \nТы можешь добавить еще {Profile.MaxPhotos - photoCount} или оставить {(photoCount > 1 ? "эти" : "эту")}.";
			await _bot.SendMessageHtml(chatId, responceMessage, keyboard, cancellationToken: ct);
		}
	}
}

