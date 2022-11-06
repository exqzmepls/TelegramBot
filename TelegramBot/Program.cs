using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot;
using TelegramBot.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var token = configuration["Token"];
var telegramBotClient = new TelegramBotClient(token);
var hostingUrl = configuration["Url"];
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
