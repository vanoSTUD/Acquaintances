using Acquaintances.Bot.API.Endpoints;
using Acquaintances.Bot.API.Options;
using Acquaintances.Bot.Application;
using Acquaintances.Bot.DAL;
using Microsoft.AspNetCore.Http.Json;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.ConfigureTelegramBot<JsonOptions>(opt => opt.SerializerOptions);

builder.Services.Configure<BotOptions>(configuration.GetSection(BotOptions.Section));
var botOptions = configuration.GetSection(BotOptions.Section).Get<BotOptions>()!;

builder.Services.AddMSSQLDbContext(configuration.GetConnectionString("MSSQL")!);
builder.Services.AddApplicationServices();

builder.Services.AddHttpClient("webhook")
	.RemoveAllLoggers()
	.AddTypedClient<ITelegramBotClient>(
		httpClient => new TelegramBotClient(botOptions.BotToken, httpClient)
	);

var app = builder.Build();

var bot = app.Services.GetRequiredService<ITelegramBotClient>();
var webhookUrl = botOptions.BotWebhookUrl.AbsoluteUri;
var secretToken = botOptions.SecretToken;

await bot.SetWebhook(webhookUrl, allowedUpdates: [], secretToken: secretToken);

app.MapBotEndpoints();

app.Run();