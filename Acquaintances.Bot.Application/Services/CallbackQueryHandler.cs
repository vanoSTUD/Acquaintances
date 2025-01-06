using Acquaintances.Bot.Application.Abstractions;
using Acquaintances.Bot.Application.Services.EntityServices;
using Acquaintances.Bot.Application.Services.UserStateHandlers;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Acquaintances.Bot.Application.Services;

public class CallbackQueryHandler : IHandler<CallbackQuery>
{
	private readonly ITelegramBotClient _bot;
	private readonly IServiceScopeFactory _scopeFactory;
	private readonly List<StateHandlerBase> _stateHandlers;

	public CallbackQueryHandler(IServiceProvider serviceProvider, IServiceScopeFactory scopeFactory, ITelegramBotClient bot)
	{
		_bot = bot;
		_scopeFactory = scopeFactory;
		_stateHandlers = serviceProvider.GetServices<StateHandlerBase>().ToList();
	}

	public async Task HandleAsync(Update update, CancellationToken ct = default)
	{
		ArgumentNullException.ThrowIfNull(update, nameof(update));
		ArgumentNullException.ThrowIfNull(update.CallbackQuery, nameof(update.CallbackQuery));

		using var scope = _scopeFactory.CreateScope();
		var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

		var chatId = update.CallbackQuery.From.Id;
		var user = await userService.GetOrCreateAsync(chatId, ct);

		if (update.CallbackQuery.Data is { } data)
		{
			await _stateHandlers.First(x => x.CallbackData == data).Execute(update, ct);
		}

		await _bot.AnswerCallbackQuery(update.CallbackQuery.Id);
	}
}
