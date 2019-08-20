using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TweetPosingBackend.Controller.Helpers;
using TweetPosingBackend.Model;

namespace TweetPosingBackend.Controller
{
    public static class TwitterController
    {
        public static async Task<bool> ShareTweet(Tweet tweet)
        {
            string baseUrl = "https://api.twitter.com/1.1/statuses/update.json";

            using(HttpClient client = new HttpClient())
            {
                string authHeader = TwitterAuthHelper.GetAuthorizationHeader(baseUrl);

                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authHeader);
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "TweetAuthAppDemo1234");
                client.DefaultRequestHeaders.Add("Host","api.twitter.com");

                StringContent requestData = new StringContent(
                    $"status={tweet.Text}",
                    Encoding.UTF8, "application/x-www-form-urlencoded");

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                string endpoint = $"{baseUrl}?screen_name=albert1bennett&status={tweet.Text}";

                var response = await client.PostAsJsonAsync(endpoint, string.Empty);

                string responseString = await response.Content.ReadAsStringAsync();

                return response.IsSuccessStatusCode;
            }
        }
    }
}
