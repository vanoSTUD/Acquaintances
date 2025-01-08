using Acquaintances.Bot.Domain.Enums;
using Acquaintances.Bot.Domain.ValueObjects.Profile;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Telegram.Bot.Types;
using Telegram.Bot;
using Acquaintances.Bot.Application.Extensions;
using Acquaintances.Bot.Application.Helpers;
using Acquaintances.Bot.Application.Services.EntityServices;

namespace Acquaintances.Bot.Application.Services.UserStateHandlers;

public class EnteringAgeHandler : StateHandlerBase
{
    private readonly ITelegramBotClient _bot;
    private readonly IServiceScopeFactory _scopeFactory;
    public EnteringAgeHandler(ITelegramBotClient botClient, IServiceScopeFactory scopeFactory)
    {
        _bot = botClient;
        _scopeFactory = scopeFactory;
    }

    public override State State => State.EnteringAge;

    public override async Task Execute(Update update, CancellationToken ct = default)
    {
        if (update.Message is not { } message)
        {
            return;
        }

        var chatId = update.GetChatId();
		var inputAge = message.Text;
        var ageResult = Age.Create(inputAge);

        if (ageResult.IsFailure)
        {
            await _bot.SendMessageHtml(chatId, ageResult.Error, cancellationToken: ct);
            return;
        }

		using var scope = _scopeFactory.CreateScope();
		var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
		var user = await userService.GetOrCreateAsync(chatId, ct);
		var tempProfile = user.TempProfile;

        if (tempProfile == null)
        {
            await _bot.SendMessageHtml(chatId, $"Ошибка! Попробуйте {CommandNames.Start}.", cancellationToken: ct);
			await userService.SetStateAsync(user, State.None, ct);
			return;
        }

		var responceMessage = $"Принято. Напиши свой город:";
        tempProfile.Age = ageResult.Value;

        await _bot.SendMessageHtml(chatId, responceMessage, cancellationToken: ct);
		await userService.SetTempProfileAsync(user, tempProfile, ct);
		await userService.SetStateAsync(user, State.EnteringCity, ct);
    }
}

