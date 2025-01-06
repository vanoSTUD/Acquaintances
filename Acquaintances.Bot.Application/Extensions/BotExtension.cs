using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Acquaintances.Bot.Application.Extensions;

public static class BotExtension
{
	public static Task<Message> SendMessageHtml(this ITelegramBotClient bot, ChatId chatId, string text, IReplyMarkup? markup = null, CancellationToken cancellationToken = default)
	{
		markup ??= new ReplyKeyboardRemove();

		return bot.SendMessage(chatId, text, parseMode: ParseMode.Html, replyMarkup: markup, cancellationToken: cancellationToken);
	}
}
