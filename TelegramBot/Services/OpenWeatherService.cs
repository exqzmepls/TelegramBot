using TelegramBot.Models;

namespace TelegramBot.Services
{
    public class OpenWeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OpenWeatherService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CityWeather?> GetWeatherAsync(string city)
        {
            var client = _httpClientFactory.CreateClient("openweathermap");

            var apiKey = Environment.GetEnvironmentVariable("OPEN_WEATHER_API_KEY");
            var uriBuilder = new UriBuilder
            {
                Scheme = "http",
                Host = "api.openweathermap.org",
                Path = string.Join('/', "data", "2.5", "weather"),
                Query = string.Join('&', $"q={city}", "units=metric", $"appid={apiKey}")
            };
            var uri = uriBuilder.Uri;

            try
            {
                var s = await client.GetStringAsync(uri);
                Console.WriteLine(s);
                var cityWeather = await client.GetFromJsonAsync<CityWeather>(uri);
                return cityWeather;
            }
            catch (Exception ex)
            {
                throw new Exception($"fail {uri}", ex);
            }

        }
    }
}
