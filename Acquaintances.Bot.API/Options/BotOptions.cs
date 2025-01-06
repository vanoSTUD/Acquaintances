namespace Acquaintances.Bot.API.Options;

public class BotOptions
{
	public const string Section = nameof(BotOptions);

	public string BotToken { get; set; } = string.Empty;
	public string SecretToken { get; set; } = string.Empty;
	public Uri BotWebhookUrl { get; set; } = null!;
}
