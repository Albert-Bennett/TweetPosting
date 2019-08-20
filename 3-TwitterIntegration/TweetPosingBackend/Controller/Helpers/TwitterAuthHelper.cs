using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TweetPosingBackend.Controller.Helpers;

namespace TweetPosingBackend.Controller.Helpers
{
    public static class TwitterAuthHelper
    {
        public static string GetAuthorizationHeader(string endpoint)
        {
            //Authorization: OAuth realm = "api.twitter.com",
            //    oauth_consumer_key = "ymgqpnrmNC9h4ZH729t9Nqiwe",
            //    oauth_token = "747585394699534336-fqWeiEF0MNTfxwWQe1pdNXqtFZa3pCO",
            //    oauth_signature_method = "HMAC-SHA1",
            //    oauth_timestamp = "1566334983", 
            //    oauth_nonce = "1234eufwiubefibwr3u53hruefsu",
            //    oauth_version = "1.0", 
            //    oauth_signature = "tYFch43p4rFfIPM8gGIFSI8Ut5Q%3D"'

            string nonce = Uri.EscapeDataString(
                Guid.NewGuid().ToString().Replace("-", string.Empty));

            string headerValue = string.Empty;//"OAuth ";

            headerValue += $"oauth_consumer_key={Uri.EscapeDataString(ConstantValues.OAuthCustomerKey)}&";
            headerValue += $"oauth_nonce={nonce}&";
            headerValue += $"oauth_signature_method={Uri.EscapeDataString(ConstantValues.SignatureMethod)}&";
            headerValue += $"oauth_timestamp={DateTimeHelper.TimestampNow()}&";
            headerValue += $"oauth_token={Uri.EscapeDataString(ConstantValues.OAuthToken)}&";
            headerValue += $"oauth_version={Uri.EscapeDataString(ConstantValues.OAuthVersion)}";
            //headerValue += $"status={Uri.EscapeDataString(tweet)}";
            //headerValue += $"oauth_signature={SignKey(headerValue)}";

            string baseKey = $"POST&{Uri.EscapeDataString(endpoint)}&{headerValue}";
            string signedKey = SignKey(baseKey);

            headerValue = $"OAuth {headerValue}&oauth_signature={signedKey}";

            return headerValue;
        }

        static string SignKey(string baseString)
        {
            string customerSecret = Uri.EscapeDataString(ConstantValues.OAuthCustomerSecret);
            string accessSecret = Uri.EscapeDataString(ConstantValues.OAuthAccessSecret);

            string key = $"{customerSecret}&{accessSecret}";

            HMACSHA1 hmac = new HMACSHA1();
            hmac.Key = Encoding.UTF8.GetBytes(key);

            byte[] encoded = Encoding.UTF8.GetBytes(baseString);
            byte[] hash = hmac.ComputeHash(encoded);

            return Convert.ToBase64String(hash);
        }
    }
}
