using Acquaintances.Bot.Application.Helpers;
using Acquaintances.Bot.Application.Services.EntityServices;
using Acquaintances.Bot.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot;
using Acquaintances.Bot.Application.Extensions;

namespace Acquaintances.Bot.Application.Services.UserStateHandlers;

public class ChangingDescriptionHandler : StateHandlerBase
{
    private readonly ITelegramBotClient _bot;
    private readonly IServiceScopeFactory _scopeFactory;
    public ChangingDescriptionHandler(ITelegramBotClient botClient, IServiceScopeFactory scopeFactory)
    {
        _bot = botClient;
        _scopeFactory = scopeFactory;
    }

    public override UserStates State => UserStates.None;
    public override string CallbackData => CallbackQueryData.ChangingDescription;

    public override async Task Handle(Update update, CancellationToken ct = default)
    {
        if (update.CallbackQuery is not { } query)
        {
            //Todo: обработка нештатной ситуации
            return;
        }

        var chatId = update.GetChatId();
        using var scope = _scopeFactory.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        var user = await userService.GetOrCreateAsync(chatId, ct);

        if (user.Profile == null)
        {
            await _bot.SendMessageHtml(chatId, $"Ошибка! У вас нет анкеты.", cancellationToken: ct);
            await userService.SetStateAsync(user, UserStates.None, ct);
            await userService.UpdateAsync(user, ct);
            return;
        }

        var responceMessage = $"Введи новое описание анкеты:";
        await _bot.SendMessageHtml(chatId, responceMessage, cancellationToken: ct);

        await userService.SetStateAsync(user, UserStates.EnteringDescription, ct);
    }
}

