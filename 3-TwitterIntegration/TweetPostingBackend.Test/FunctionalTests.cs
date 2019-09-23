using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using System.Threading.Tasks;
using TweetPosingBackend;
using TweetPostingBackend.Test.Helpers;

namespace TweetPostingBackend.Test
{
    public class FunctionalTests
    {
        [Test]
        public async Task The_Function_Should_Throw_A_Bad_Request_When_We_Dont_Pass_In_A_Request_Body()
        {
            EnvironmentHelper.SetUpEnvironmentVariables();

            var req = HttpRequestCreator.CreateRequest();
            var logger = NullLogger.Instance;

            var response = await SharePost.Run(req, logger);

            Assert.IsTrue(response is BadRequestObjectResult);
        }

        [TestCase("", "")]
        [TestCase("", "this is test content")]
        [TestCase("www.google.com", "")]
        public async Task The_Function_Should_Throw_A_Bad_Request_When_We_Dont_Pass_In_Data_For_Properties(
            string postUrl, string postContent)
        {
            EnvironmentHelper.SetUpEnvironmentVariables();

            var req = await HttpRequestCreator.CreateRequest(postUrl, postContent);
            var logger = NullLogger.Instance;

            var response = await SharePost.Run(req, logger);

            Assert.IsTrue(response is BadRequestObjectResult);
        }

        [Test]
        public async Task The_Function_Should_Return_An_Ok_Object_Result_When_We_Pass_In_A_Valid_Request_Body()
        {
            EnvironmentHelper.SetUpEnvironmentVariables();

            var req = await HttpRequestCreator.CreateRequest("www.google.com", "This is test content.");
            var logger = NullLogger.Instance;

            var response = await SharePost.Run(req, logger);

            Assert.IsTrue(response is OkObjectResult);
        }
    }
}
