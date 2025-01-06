using Telegram.Bot.Types;

namespace Acquaintances.Bot.Application.Abstractions;

public interface IHandler<T>
{
    public Task HandleAsync(Update update, CancellationToken ct = default);
}
