using Acquaintances.Bot.API.Services;
using Telegram.Bot.Types;

namespace Acquaintances.Bot.API.Endpoints;

public static class BotEndpoints
{
	public static void MapBotEndpoints(this IEndpointRouteBuilder builder)
	{
		builder.MapPost("update", HandleUpdate);
	}

	private static async Task<IResult> HandleUpdate(Update update, UpdateHandler updateHandler, ExceptionHandler exceptionHandler, CancellationToken ct = default)
	{
		try
		{
			await updateHandler.HandleAsync(update, ct);
		}
		catch (Exception ex)
		{
			exceptionHandler.Handle(ex);
		}

		return Results.Ok();
	}
}
