using Acquaintances.Bot.API.Options;
using Acquaintances.Bot.Application.Abstractions;
using Acquaintances.Bot.Application.Services;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;

namespace Acquaintances.Bot.API.Endpoints;

public static class BotEndpoints
{
	public static void MapBotEndpoints(this IEndpointRouteBuilder builder)
	{
		builder.MapPost("/", HandleUpdate);
	}

	private static async Task<IResult> HandleUpdate(
		Update update,
		IHandler<Update> updateHandler,
		ExceptionHandler exceptionHandler,
		IOptions<BotOptions> botOptions,
		HttpContext context,
		CancellationToken ct = default)
	{
        if (context.Request.Headers["X-Telegram-Bot-Api-Secret-Token"] != botOptions.Value.SecretToken)
        {
            return Results.Forbid();
        }

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
