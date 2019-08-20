using NUnit.Framework;
using System.Threading.Tasks;
using TweetPosingBackend.Controller;
using TweetPosingBackend.Model;
using TweetPostingBackend.Test.Helpers;

namespace TweetPostingBackend.Test
{
    public class TweetingTests
    {
        [TestCase("This tweet has no url attached to it","")]
        [TestCase("This tweet does have a url attached to it", "https://www.google.ie")]
        public async Task It_Should_Send_A_Tweet_When_A_Valid_Tweet_Object_Is_Sent(string message, string url)
        {
            EnvironmentHelper.SetUpEnvironmentVariables();

            Tweet tweet = new Tweet()
            {
                Text = message,
                Urls = string.IsNullOrEmpty(url) ?
                  null : new TwitterUrl[]
                  {
                      new TwitterUrl
                      {
                           Url = url
                      }
                  }
            };

            Assert.IsTrue(await TwitterController.ShareTweet(tweet));
        } 
    }
}
