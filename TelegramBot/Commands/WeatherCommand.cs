using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Services;

namespace TelegramBot.Commands
{
    public class WeatherCommand : ICommand
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IWeatherService _weatherService;

        public WeatherCommand(ITelegramBotClient telegramBotClient, IWeatherService weatherService)
        {
            _telegramBotClient = telegramBotClient;
            _weatherService = weatherService;
        }

        public async Task ExecuteAsync(Message requestMessage)
        {
            var trimmedText = requestMessage.Text!.Trim();
            var regex = new Regex(@"\/weather (\p{Lu}.*)");
            var match = regex.Match(trimmedText);
            var cityGroup = match.Groups.Values.Skip(1).Single();
            var city = cityGroup.Value;

            var weather = await _weatherService.GetWeatherAsync(city);
            var chatId = requestMessage.Chat.Id;
            if (weather == null)
            {
                await _telegramBotClient.SendTextMessageAsync(chatId, $"Impossible to get weather for {city}.");
                return;
            }

            var main = weather.Main;
            var pressure = Math.Round(main.Pressure / 1.333);
            var responseText = $"{city}: {weather.Weather.First().Main}. Temperature is {main.Temperature}°C (feels like {main.FeelsLike}°C). The wind speed is {weather.Wind.Speed} m/s. Atmospheric pressure is {pressure} mm Hg.";
            await _telegramBotClient.SendTextMessageAsync(chatId, responseText);
        }

        public bool IsRequestedByMessage(string? text)
        {
            if (text == null)
                return false;

            var trimmedText = text.Trim();
            var regex = new Regex(@"\/weather (\p{Lu}.*)");
            var isMatch = regex.IsMatch(trimmedText);
            return isMatch;
        }
    }
}
