using Newtonsoft.Json;

namespace Telegram.Bot.Examples.DotNetCoreWebHook.Models
{
    public class WebhookResponse
    {
        [JsonProperty("ok")]
        public bool OK { get; set; }
        [JsonProperty("result")]
        public bool Result { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
