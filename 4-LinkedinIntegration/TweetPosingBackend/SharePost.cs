using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using TweetPosingBackend.Controller;
using TweetPosingBackend.Model;

namespace TweetPosingBackend
{
    public static class SharePost
    {
        [FunctionName("SharePost")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogWarning("We called it!!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            if (!string.IsNullOrEmpty(requestBody))
            {
                var postData = JsonConvert.DeserializeObject<SharedPostData>(requestBody);

                if (string.IsNullOrEmpty(postData.PostContent))
                    return new BadRequestObjectResult("Pass in a value for the Property PostContent");

                if (string.IsNullOrEmpty(postData.Url))
                    return new BadRequestObjectResult("Pass in a value for the Property PostUrl");

                bool successfullySharedPost = false;

                switch (postData.IntegrationType)
                {
                    case IntegrationTypes.Twitter:
                        successfullySharedPost = await TwitterController.ShareTweet(postData.ToTweet());
                        break;

                    case IntegrationTypes.Both:
                        successfullySharedPost = await TwitterController.ShareTweet(postData.ToTweet());
                        break;
                }

                if (successfullySharedPost)
                    return new OkObjectResult("All good");
                else
                    return new BadRequestObjectResult("There was an issure with sharing your post");
            }

            return new BadRequestObjectResult("Pass in a request body.");
        }
    }
}
