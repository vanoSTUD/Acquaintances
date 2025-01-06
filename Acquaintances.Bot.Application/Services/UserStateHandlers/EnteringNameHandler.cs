﻿using Acquaintances.Bot.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot;
using Acquaintances.Bot.Application.Extensions;
using Acquaintances.Bot.Domain.ValueObjects.Profile;
using Acquaintances.Bot.Application.Services.EntityServices;
using Acquaintances.Bot.Domain.Models;

namespace Acquaintances.Bot.Application.Services.UserStateHandlers;

public class EnteringNameHandler : StateHandlerBase
{
    private readonly ITelegramBotClient _bot;
    private readonly IServiceScopeFactory _scopeFactory;
    public EnteringNameHandler(ITelegramBotClient botClient, IServiceScopeFactory scopeFactory)
    {
        _bot = botClient;
        _scopeFactory = scopeFactory;
    }

    public override State State => State.EnteringName;

    public override async Task Execute(Update update, CancellationToken ct = default)
    {
		if (update.Message is not { } message)
		{
			return;
		}

        var nameResult = Name.Create(message.Text);
        var chatId = update.GetChatId();

        if (nameResult.IsFailure)
        {
            await _bot.SendMessageHtml(chatId, nameResult.Error, cancellationToken: ct);
            return;
        }

		using var scope = _scopeFactory.CreateScope();
		var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
		var user = await userService.GetOrCreateAsync(chatId, ct);

		var responceMessage = $"Отлично, <b>{nameResult.Value}</b>! \nНапиши свой возраст:";
        await _bot.SendMessageHtml(chatId, responceMessage, cancellationToken: ct);

        var tempProfile = new TempProfile() { Name = nameResult.Value };
		await userService.SetTempProfileAsync(user, tempProfile, ct);
		await userService.SetStateAsync(user, State.EnteringAge, ct);
    }
}

