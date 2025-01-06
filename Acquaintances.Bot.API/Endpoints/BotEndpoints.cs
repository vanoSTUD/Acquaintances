using Acquaintances.Bot.Application.Abstractions;
using Acquaintances.Bot.Application.Services;
using Telegram.Bot.Types;

namespace Acquaintances.Bot.API.Endpoints;

public static class BotEndpoints
{
	public static void MapBotEndpoints(this IEndpointRouteBuilder builder)
	{
		builder.MapPost("/", HandleUpdate);
	}

	private static async Task<IResult> HandleUpdate(Update update, IHandler<Update> updateHandler, ExceptionHandler exceptionHandler, CancellationToken ct = default)
	{
		try
		{
			await updateHandler.HandleAsync(update, ct);
		}
		catch (Exception ex)
		{
			await exceptionHandler.HandleAsync(ex, ct);
		}

		return Results.Ok();
	}
}
