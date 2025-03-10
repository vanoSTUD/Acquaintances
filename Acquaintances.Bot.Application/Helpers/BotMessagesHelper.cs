﻿using Acquaintances.Bot.Application.Extensions;
using Acquaintances.Bot.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Acquaintances.Bot.Application.Helpers;

public static class BotMessagesHelper
{
	public static async Task SendProfileAsync(ITelegramBotClient bot, long chatId, Profile? profile, CancellationToken ct = default)
	{
		await ShowProfile(bot, chatId, profile, ct);
		await ShowProfileCommands(bot, chatId, ct);
	}

	public static async Task SendNoProfileMessageAsync(ITelegramBotClient bot, long chatId,CancellationToken ct = default)
	{
		await bot.SendMessageHtml(chatId, $"Ошибка! У тебя нет анкеты. Попробуй {CommandNames.Start}", cancellationToken: ct);
	}

	public static async Task SendErrorMessageAsync(ITelegramBotClient bot, long chatId, CancellationToken ct = default)
	{
		await bot.SendMessageHtml(chatId, $"Ошибка! Попробуй {CommandNames.Start}", cancellationToken: ct);
	}

	public static InlineKeyboardMarkup GetStopChangesKeyboard()
	{
		return new InlineKeyboardMarkup().AddButton("Не изменять", CallbackQueryData.ViewMyProfile);
	}

	public static InlineKeyboardMarkup GetKeepCurrentKeyboard()
	{
		return new InlineKeyboardMarkup().AddButton("Оставить текущее", "");
	}

	private static async Task ShowProfileCommands(ITelegramBotClient bot, long chatId, CancellationToken ct = default)
	{
		var commands = """
			1 - Смотреть анкеты
			2 - Заполнить анкету заново
			3 - Изменить описание анкеты
			4 - Изменить фото анкеты
			""";
		var keyboard = new InlineKeyboardMarkup()
			.AddButton("1")
			.AddButton("2", CallbackQueryData.CreateProfile)
			.AddButton("3", CallbackQueryData.ChangingDescription)
			.AddButton("4", CallbackQueryData.ChangingPhotos);

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
