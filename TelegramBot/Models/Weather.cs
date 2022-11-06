using Newtonsoft.Json;

namespace TelegramBot.Models
{
    public class Weather
    {
        [JsonProperty("main", Required = Required.Always)]
        public string Main { get; set; } = null!;
    }
}
