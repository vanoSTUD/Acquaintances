
using Microsoft.Extensions.Logging;

namespace Acquaintances.Bot.Application.Services;

public class ExceptionHandler
{
	private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(ILogger<ExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(Exception exception, CancellationToken ct = default)
	{
        _logger.LogError("{Exception}", exception);

        await Task.CompletedTask;
	}
}
