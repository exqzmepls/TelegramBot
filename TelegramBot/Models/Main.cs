using Newtonsoft.Json;

namespace TelegramBot.Models
{
    public class Main
    {
        [JsonProperty("temp", Required = Required.Always)]
        public double Temperature { get; set; }

        [JsonProperty("feels_like", Required = Required.Always)]
        public double FeelsLike { get; set; }

        [JsonProperty("pressure", Required = Required.Always)]
        public int Pressure { get; set; }
    }
}
