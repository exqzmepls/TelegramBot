using System.Collections.Specialized;
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
            var resourcePath = string.Join('/', "data", "2.5", "weather");
            var paramsCollection = new NameValueCollection
            {
                { "q", city },
                { "units", "metric" },
                { "appid",  apiKey }
             };
            var uriBuilder = new UriBuilder
            {
                Scheme = "http",
                Host = "api.openweathermap.org",
                Path = resourcePath,
                Query = paramsCollection.ToString()
            };
            var uri = uriBuilder.Uri;

            try
            {
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
