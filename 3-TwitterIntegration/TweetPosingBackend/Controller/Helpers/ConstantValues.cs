using System;
using System.Collections.Generic;
using System.Text;

namespace TweetPosingBackend.Controller.Helpers
{
    public static class ConstantValues
    {
        public static string OAuthVersion => Environment.GetEnvironmentVariable("OAUTH_VERSION");
        public static string OAuthToken => Environment.GetEnvironmentVariable("OAUTH_TOKEN");
        public static string OAuthCustomerKey => Environment.GetEnvironmentVariable("OAUTH_CUSTOMER_KEY");
        public static string SignatureMethod => Environment.GetEnvironmentVariable("SIGNATURE_METHOD");
        public static string OAuthCustomerSecret => Environment.GetEnvironmentVariable("OAUTH_CUSTOMER_SECRET");
        public static string OAuthAccessSecret => Environment.GetEnvironmentVariable("OAUTH_ACCESS_SECRET");
    }
}
