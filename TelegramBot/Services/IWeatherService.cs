using TelegramBot.Models;

namespace TelegramBot.Services
{
    public interface IWeatherService
    {
        public Task<CityWeather?> GetWeatherAsync(string city);
    }
}
