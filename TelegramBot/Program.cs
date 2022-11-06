using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot;
using TelegramBot.Services;

var builder = WebApplication.CreateBuilder(args);

var token = GetEnvironmentVariable("TG_API_TOKEN");
var telegramBotClient = new TelegramBotClient(token);
var hostingUrl = GetEnvironmentVariable("HOST_URL");
var webHookUrl = hostingUrl + "api/update";
await telegramBotClient.SetWebhookAsync(webHookUrl);

// Add services to the container.

builder.Services.AddScoped<ICommandsService, CommandsService>();
builder.Services.AddSingleton<ITelegramBotClient>(telegramBotClient);
builder.Services.AddSingleton<IWeatherService, OpenWeatherService>();
builder.Services.AddHttpClient("openweathermap", client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
});
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    var settings = options.SerializerSettings;
    settings.Formatting = Formatting.Indented;
    settings.ContractResolver = new DefaultContractResolver();
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

string GetEnvironmentVariable(string name) => Environment.GetEnvironmentVariable(name) ?? string.Empty;