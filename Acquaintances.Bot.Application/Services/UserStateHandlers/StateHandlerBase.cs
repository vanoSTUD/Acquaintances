using Acquaintances.Bot.Domain.Enums;
using Telegram.Bot.Types;

namespace Acquaintances.Bot.Application.Services.UserStateHandlers;

public abstract class StateHandlerBase
{
	public abstract State State { get; }
	public virtual string CallbackData { get; } = string.Empty;

	public abstract Task Execute(Update update, CancellationToken ct = default);
}
