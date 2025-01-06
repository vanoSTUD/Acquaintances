using Acquaintances.Bot.Application.Helpers;
using Acquaintances.Bot.Domain.Enums;
using Acquaintances.Bot.Domain.ValueObjects.Profile;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot;
using Acquaintances.Bot.Application.Extensions;
using Acquaintances.Bot.Application.Services.EntityServices;
using Telegram.Bot.Types.ReplyMarkups;

namespace Acquaintances.Bot.Application.Services.UserStateHandlers;

public class EnteringPreferedGenderHandler : StateHandlerBase
{
	private readonly ITelegramBotClient _bot;
	private readonly IServiceScopeFactory _scopeFactory;
	public EnteringPreferedGenderHandler(ITelegramBotClient botClient, IServiceScopeFactory scopeFactory)
	{
		_bot = botClient;
		_scopeFactory = scopeFactory;
	}

	public override State State => State.EnteringPrefferedGender;

	public override async Task Execute(Update update, CancellationToken ct = default)
	{
		if (update.Message is not { } message)
		{
			return;
		}

		var chatId = update.GetChatId();
		var inputPreferedGender = message.Text;
		var preferedGenderResult = Gender.Create(inputPreferedGender, true);

		if (preferedGenderResult.IsFailure)
		{
			var failKeyboard = new ReplyKeyboardMarkup()
			{
				ResizeKeyboard = true,
				OneTimeKeyboard = true
			};
			failKeyboard.AddButton(Gender.Male.Value).AddButton(Gender.Female.Value).AddButton(Gender.All.Value);
			await _bot.SendMessageHtml(chatId, preferedGenderResult.Error, failKeyboard, cancellationToken: ct);
			return;
		}

		using var scope = _scopeFactory.CreateScope();
		var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
		var user = await userService.GetOrCreateAsync(chatId, ct);
		var tempProfile = user.GetTempProfile();

		if (tempProfile == null)
		{
			await _bot.SendMessageHtml(chatId, $"Ошибка! Попробуй {CommandNames.Start}.", cancellationToken: ct);
			await userService.SetStateAsync(user, State.None, ct);
			return;
		}

		var responceMessage = $"Следом напиши немного о себе:";
		await _bot.SendMessageHtml(chatId, responceMessage, cancellationToken: ct);

		tempProfile.PreferredGender = preferedGenderResult.Value;
		await userService.SetTempProfileAsync(user, tempProfile, ct);
		await userService.SetStateAsync(user, State.EnteringDescription, ct);
	}
}

