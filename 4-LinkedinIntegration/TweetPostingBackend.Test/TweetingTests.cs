using NUnit.Framework;
using System.Threading.Tasks;
using TweetPosingBackend.Controller;
using TweetPosingBackend.Model;
using TweetPostingBackend.Test.Helpers;

namespace TweetPostingBackend.Test
{
    public class TweetingTests
    {
        [TestCase("This tweet has no url attached to it")]
        public async Task It_Should_Send_A_Tweet_When_A_Valid_Tweet_Object_Is_Sent(string message)
        {
            EnvironmentHelper.SetUpEnvironmentVariables();

            Tweet tweet = new Tweet()
            {
                Text = message
            };

            Assert.IsTrue(await TwitterController.ShareTweet(tweet));
        } 

        [TestCase]
        public async Task It_Should_Not_Send_A_Tweet_When_A_Tweet_With_No_Text_Is_Sent()
        {
            EnvironmentHelper.SetUpEnvironmentVariables();

            Tweet tweet = new Tweet()
            {
                Text = string.Empty
            };

            Assert.IsTrue(await TwitterController.ShareTweet(tweet));
        }
    }
}
