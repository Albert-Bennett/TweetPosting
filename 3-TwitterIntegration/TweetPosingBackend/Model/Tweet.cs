using Newtonsoft.Json;

namespace TweetPosingBackend.Model
{
    public class Tweet
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("urls")]
        public TwitterUrl[]Urls { get; set; }
    }

    public class TwitterUrl
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
