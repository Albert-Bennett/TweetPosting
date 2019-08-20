using Newtonsoft.Json;

namespace TweetPosingBackend.Model
{
    public class SharedPostData
    {
        [JsonProperty("PostUrl")]
        public string Url { get; set; }

        [JsonProperty("PostContent")]
        public string PostContent { get; set; }

        [JsonProperty("IntegrationType")]
        public IntegrationTypes IntegrationType { get; set; }
    }
}
