using Newtonsoft.Json;

namespace TelegramBot.Models
{
    public class CityWeather
    {
        [JsonProperty("weather", Required = Required.Always)]
        public Weather Weather { get; set; } = null!;

        [JsonProperty("main", Required = Required.Always)]
        public Main Main { get; set; } = null!;

        [JsonProperty("wind", Required = Required.Always)]
        public Wind Wind { get; set; } = null!;
    }
}
