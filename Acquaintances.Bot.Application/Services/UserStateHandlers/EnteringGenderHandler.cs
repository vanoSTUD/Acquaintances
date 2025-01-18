using Acquaintances.Bot.Application.Extensions;
using Acquaintances.Bot.Application.Helpers;
using Acquaintances.Bot.Application.Services.EntityServices;
using Acquaintances.Bot.Domain.Enums;
using Acquaintances.Bot.Domain.ValueObjects.Profile;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Acquaintances.Bot.Application.Services.UserStateHandlers;

public class EnteringGenderHandler : StateHandlerBase
{
	private readonly ITelegramBotClient _bot;
	private readonly IServiceScopeFactory _scopeFactory;
	public EnteringGenderHandler(ITelegramBotClient botClient, IServiceScopeFactory scopeFactory)
	{
		_bot = botClient;
		_scopeFactory = scopeFactory;
	}

	public override UserStates State => UserStates.EnteringGender;

	public override async Task Handle(Update update, CancellationToken ct = default)
	{
		if (update.Message is not { } message)
		{
			return;
		}

		var chatId = update.GetChatId();
		var inputGender = message.Text;
		var genderResult = Gender.Create(inputGender, false);

		if (genderResult.IsFailure)
		{
			var failKeyboard = new ReplyKeyboardMarkup()
			{
				ResizeKeyboard = true,
				OneTimeKeyboard = true
			};
			failKeyboard.AddButton(Gender.Male.Value).AddButton(Gender.Female.Value);
			await _bot.SendMessageHtml(chatId, genderResult.Error, failKeyboard, cancellationToken: ct);
			return;
		}

		using var scope = _scopeFactory.CreateScope();
		var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
		var user = await userService.GetOrCreateAsync(chatId, ct);
		var tempProfile = user.TempProfile;

		if (tempProfile == null)
		{
			await _bot.SendMessageHtml(chatId, $"Ошибка! Попробуйте {CommandNames.Start}.", cancellationToken: ct);
			await userService.SetStateAsync(user, UserStates.None, ct);
			return;
		}

		var keyboard = new ReplyKeyboardMarkup()
		{
			ResizeKeyboard = true,
			OneTimeKeyboard = true
		};
		keyboard.AddButton(Gender.Male.Value).AddButton(Gender.Female.Value).AddButton(Gender.All.Value);

		var responceMessage = $"Супер! Кого ищем?";
		await _bot.SendMessageHtml(chatId, responceMessage, keyboard, ct);

		tempProfile.Gender = genderResult.Value;
		await userService.SetTempProfileAsync(user, tempProfile, ct);
		await userService.SetStateAsync(user, UserStates.EnteringPrefferedGender, ct);
	}
}

