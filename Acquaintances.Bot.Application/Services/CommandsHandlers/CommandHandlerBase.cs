using Telegram.Bot.Types;

namespace Acquaintances.Bot.Application.Services.CommandsHandlers;

public abstract class CommandHandlerBase
{
	public abstract string CommandName { get; }

	public abstract Task Handle(Update update, CancellationToken ct = default);
}
