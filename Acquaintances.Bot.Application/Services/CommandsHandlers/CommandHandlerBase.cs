using Acquaintances.Bot.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Acquaintances.Bot.Application.Services.CommandsHandlers;

public abstract class CommandHandlerBase
{
    public abstract string CommandName { get; }

    public abstract Task Execute(Update update, AppUser user, ITelegramBotClient bot, CancellationToken ct = default);
}
