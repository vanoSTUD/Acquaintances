using Acquaintances.Bot.Application.Extensions;
using Acquaintances.Bot.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Acquaintances.Bot.Application.Helpers;

public static class BotMessagesHelper
{
	public static async Task SendProfileCommands(ITelegramBotClient bot, long chatId)
	{
		var commands = """
			1 - Смотреть анкеты
			2 - Заполнить анкету заново
			3 - Изменить текст анкеты
			4 - Изменить фото/видео анкеты
			""";
		var keyboard = new InlineKeyboardMarkup();
		keyboard.AddButton("1").AddButton("2", CallbackQueryData.CreateProfile).AddButton("3").AddButton("4");

		await bot.SendMessageHtml(chatId, commands, keyboard);
	}

	public static async Task SendProfile(ITelegramBotClient bot, long chatId, Profile? profile)
	{
		if (profile == null)
			return;

		var photoIds = profile.Photos.Select(p => p.FileId).ToArray();
		var photoGroup = photoIds.Select(p => new InputMediaPhoto(p)).ToList();
		photoGroup.First().Caption = profile.GetFullCaption();

		await bot.SendMessageHtml(chatId, "Вот твоя анкета:");
		await bot.SendMediaGroup(chatId, photoGroup);
	}
}
