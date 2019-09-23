using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TweetPosingBackend.Controller.Helpers;
using TweetPosingBackend.Model;

namespace TweetPosingBackend.Controller
{
    public static class TwitterController
    {
        public static async Task<bool> ShareTweet(Tweet tweet)
        {
            string baseUrl = ConstantValues.TwiterPostUrl;

            using(HttpClient client = new HttpClient())
            {
                Tuple<string, FormUrlEncodedContent> data =
                    TwitterAuthHelper.GetAuthorizationHeader(baseUrl, tweet.Text);

                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", data.Item1);
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", ConstantValues.TwitterUserAgent);
                client.DefaultRequestHeaders.Add("Host", ConstantValues.TwitterHostApi);

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync(baseUrl, data.Item2);

                string responseString = await response.Content.ReadAsStringAsync();

                return response.IsSuccessStatusCode;
            }
        }
    }
}
