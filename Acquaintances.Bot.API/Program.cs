using Acquaintances.Bot.API.Endpoints;
using Acquaintances.Bot.API.Services;
using Acquaintances.Bot.DAL;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddMSSQLDbContext(configuration.GetConnectionString("MSSQL")!);

builder.Services.AddSingleton<UpdateHandler>();
builder.Services.AddSingleton<ExceptionHandler>();

builder.Services.AddHttpClient("webhook").RemoveAllLoggers().AddTypedClient<ITelegramBotClient>(
	httpClient => new TelegramBotClient("7749276508:AAEBgYVWW3QrXHb6kI1kAsJBnDr1cDlI8g0", httpClient));

var app = builder.Build();

var bot = app.Services.GetRequiredService<ITelegramBotClient>();
var webhookUrl = "https://fbb3-95-24-53-51.ngrok-free.app/update";
var secretToken = "asdda";

await bot.SetWebhook(webhookUrl, allowedUpdates: [], secretToken: secretToken);

app.MapBotEndpoints();

app.Run();
