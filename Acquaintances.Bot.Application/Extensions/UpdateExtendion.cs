using Telegram.Bot.Types;

namespace Acquaintances.Bot.Application.Extensions;

public static class UpdateExtendion
{
	public static long GetChatId(this Update update)
	{
		return update switch
		{
			{ Message.Chat.Id: var id } => id,
			{ CallbackQuery.Message.Chat.Id: var id } => id,
			{ MessageReactionCount.Chat.Id: var id } => id,
			{ MessageReaction.Chat.Id: var id } => id,
			{ EditedMessage.Chat.Id: var id } => id,
			_ => throw new Exception($"Не удалось получить id чата: {@update}")
		};
	}
}
