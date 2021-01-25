using Newtonsoft.Json;

namespace TweetPosingBackend.Model
{
    public class Tweet
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
