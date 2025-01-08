using Acquaintances.Bot.Application.Abstractions;
using Acquaintances.Bot.Application.Helpers;
using Acquaintances.Bot.Application.Services.CommandsHandlers;
using Acquaintances.Bot.Application.Services.EntityServices;
using Acquaintances.Bot.Application.Services.UserStateHandlers;
using Acquaintances.Bot.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Acquaintances.Bot.Application.Services;

public class MessageHandler : IHandler<Message>
{
	private readonly ITelegramBotClient _bot;
	private readonly IServiceScopeFactory _scopeFactory;
	private readonly List<StateHandlerBase> _stateHandlers;
	private readonly List<CommandHandlerBase> _commandHandlers;

	public MessageHandler(IServiceProvider serviceProvider, IServiceScopeFactory scopeFactory, ITelegramBotClient bot)
	{
		_bot = bot;
		_scopeFactory = scopeFactory;
		_stateHandlers = serviceProvider.GetServices<StateHandlerBase>().ToList();
		_commandHandlers = serviceProvider.GetServices<CommandHandlerBase>().ToList();
	}

	public async Task HandleAsync(Update update, CancellationToken ct = default)
	{
		ArgumentNullException.ThrowIfNull(update.Message, nameof(update.Message));

		using var scope = _scopeFactory.CreateScope();
		var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

		var chatId = update.Message.Chat.Id;
		var user = await userService.GetOrCreateAsync(chatId, ct);

		if (update.Message.Text is { } messageText && messageText.StartsWith('/'))
		{
			string command = messageText;
			await _commandHandlers.First(x => x.CommandName == command).Execute(update, user, _bot, ct);
			return;
		}

		if (user.State != State.None)
		{
			await _stateHandlers.First(x => x.State == user.State).Execute(update, ct);
		}
		else
		{
			await _commandHandlers.First(x => x.CommandName == CommandNames.Start).Execute(update, user, _bot, ct);
		}
	}
}
