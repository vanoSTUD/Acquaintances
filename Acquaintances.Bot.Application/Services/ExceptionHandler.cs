
namespace Acquaintances.Bot.Application.Services;

public class ExceptionHandler
{
	public async Task HandleAsync(Exception exception, CancellationToken ct = default)
	{
		Console.WriteLine(exception);

		await Task.CompletedTask;
	}
}
