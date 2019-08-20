using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TweetPosingBackend.Model;

namespace TweetPostingBackend.Test.Helpers
{
    public static class HttpRequestCreator
    {
        public static HttpRequest CreateRequest()
        {
            var httpContext = new DefaultHttpContext();

            return httpContext.Request;
        }

        public static async Task<HttpRequest> CreateRequest(string postUrl, string postContent)
        {
            var httpContext = new DefaultHttpContext();

            var data = new SharedPostData()
            {
                IntegrationType = IntegrationTypes.Both,
                PostContent = postContent,
                Url = postUrl
            };

            var json = JsonConvert.SerializeObject(data);
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

            httpContext.Request.Body = await requestContent.ReadAsStreamAsync();
            httpContext.Request.ContentType = "application/json";

            return httpContext.Request;
        }
    }
}
