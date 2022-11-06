using Newtonsoft.Json;

namespace TelegramBot.Models
{
    public class Wind
    {
        [JsonProperty("speed", Required = Required.Always)]
        public double Speed { get; set; }
    }
}
