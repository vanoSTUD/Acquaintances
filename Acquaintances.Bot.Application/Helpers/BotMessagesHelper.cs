using Acquaintances.Bot.Application.Extensions;
using Acquaintances.Bot.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Acquaintances.Bot.Application.Helpers;

public static class BotMessagesHelper
{
	public static async Task ShowProfileAsync(ITelegramBotClient bot, long chatId, Profile? profile, CancellationToken ct = default)
	{
		await ShowProfile(bot, chatId, profile, ct);
		await ShowProfileCommands(bot, chatId, ct);
	}

	private static async Task ShowProfileCommands(ITelegramBotClient bot, long chatId, CancellationToken ct = default)
	{
		var commands = """
			1 - Смотреть анкеты
			2 - Заполнить анкету заново
			3 - Изменить описание анкеты
			4 - Изменить фото/видео анкеты
			""";
		var keyboard = new InlineKeyboardMarkup()
			.AddButton("1")
			.AddButton("2", CallbackQueryData.CreateProfile)
			.AddButton("3", CallbackQueryData.ChangingDescription)
			.AddButton("4");

		await bot.SendMessageHtml(chatId, commands, keyboard, cancellationToken: ct);
	}

	private static async Task ShowProfile(ITelegramBotClient bot, long chatId, Profile? profile, CancellationToken ct = default)
	{
		if (profile == null)
			return;

		var photoIds = profile.Photos.Select(p => p.FileId).ToArray();
		var photoGroup = photoIds.Select(p => new InputMediaPhoto(p)).ToList();
		photoGroup.First().Caption = profile.GetFullCaption();

		await bot.SendMessageHtml(chatId, "Твоя анкета:", cancellationToken: ct);
		await bot.SendMediaGroup(chatId, photoGroup, cancellationToken: ct);
	}
}
