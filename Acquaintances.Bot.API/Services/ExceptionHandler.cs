namespace Acquaintances.Bot.API.Services;

public class ExceptionHandler
{
	public void Handle(Exception exception)
	{
        Console.WriteLine(exception.Message);
	}
}
