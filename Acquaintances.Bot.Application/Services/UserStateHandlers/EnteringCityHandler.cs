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

public class EnteringCityHandler : StateHandlerBase
{
	private readonly ITelegramBotClient _bot;
	private readonly IServiceScopeFactory _scopeFactory;
	public EnteringCityHandler(ITelegramBotClient botClient, IServiceScopeFactory scopeFactory)
	{
		_bot = botClient;
		_scopeFactory = scopeFactory;
	}

	public override State State => State.EnteringCity;

	public override async Task Execute(Update update, CancellationToken ct = default)
	{
		if (update.Message is not { } message)
		{
			return;
		}

		var chatId = update.GetChatId();
		var inputCity = message.Text;
		var cityResult = City.Create(inputCity);


		if (cityResult.IsFailure)
		{
			await _bot.SendMessageHtml(chatId, cityResult.Error, cancellationToken: ct);
			return;
		}

		using var scope = _scopeFactory.CreateScope();
		var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
		var user = await userService.GetOrCreateAsync(chatId, ct);
		var tempProfile = user.TempProfile;

		if (tempProfile == null)
		{
			await _bot.SendMessageHtml(chatId, $"Ошибка! Попробуйте {CommandNames.Start}.", cancellationToken: ct);
			return;
		}

		var keyboard = new ReplyKeyboardMarkup()
		{
			ResizeKeyboard = true,
			OneTimeKeyboard = true
		};
		keyboard.AddButton(Gender.Male.Value).AddButton(Gender.Female.Value);

		var responceMessage = $"Разберемся с твоим полом:";
		await _bot.SendMessageHtml(chatId, responceMessage, keyboard, ct);

		tempProfile.City = cityResult.Value;
		await userService.SetTempProfileAsync(user, tempProfile, ct);
		await userService.SetStateAsync(user, State.EnteringGender, ct);
	}
}


