using Acquaintances.Bot.Application.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Acquaintances.Bot.Application.Services;

public class UpdateHandler : IHandler<Update>
{
	private readonly ITelegramBotClient _bot;
	private readonly IHandler<Message> _messageHandler;
	private readonly IHandler<CallbackQuery> _callbackQueryHandler;

	public UpdateHandler(ITelegramBotClient bot, IHandler<Message> messageHandler, IHandler<CallbackQuery> callbackQueryHandler)
	{
		_bot = bot;
		_messageHandler = messageHandler;
		_callbackQueryHandler = callbackQueryHandler;
	}

	public async Task HandleAsync(Update update, CancellationToken ct = default)
	{
		if (update == null)
			return;

		await (update.Type switch
		{
			UpdateType.Message => _messageHandler.HandleAsync(update, ct),
			UpdateType.CallbackQuery => _callbackQueryHandler.HandleAsync(update, ct),
			_ => Task.CompletedTask
		});
	}
}
