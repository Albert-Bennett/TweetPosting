using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace TweetPosingBackend.Controller.Helpers
{
    public static class TwitterAuthHelper
    {
        public static Tuple<string, FormUrlEncodedContent> GetAuthorizationHeader(string endpoint, string status)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "status", status},
                { "oauth_consumer_key", ConstantValues.OAuthCustomerKey},
                { "oauth_signature_method", ConstantValues.SignatureMethod },
                { "oauth_timestamp", DateTimeHelper.ConvertToUnixTimestamp(DateTime.Now).ToString()},
                { "oauth_nonce", Uri.EscapeDataString(Guid.NewGuid().ToString().Replace("-", string.Empty)) },
                { "oauth_token", ConstantValues.OAuthToken},
                { "oauth_version", ConstantValues.OAuthVersion }
            };

            data.Add("oauth_signature", GenerateOauthSignature(endpoint, data));
            string oAuthHeader = GenerateOAuthHeader(data);

            FormUrlEncodedContent formData = 
                new FormUrlEncodedContent(data.Where(kvp => !kvp.Key.StartsWith("oauth_")));

            return new Tuple<string, FormUrlEncodedContent>(oAuthHeader, formData);
        }

        static string GenerateOauthSignature(string url, Dictionary<string, string> data)
        {
            var sigString = string.Join("&",
                data.Union(data)
                    .Select(kvp => string.Format("{0}={1}", Uri.EscapeDataString(kvp.Key), Uri.EscapeDataString(kvp.Value)))
                    .OrderBy(s => s));

            string fullSigData = string.Format("{0}&{1}&{2}", "POST",
                Uri.EscapeDataString(url),
                Uri.EscapeDataString(sigString.ToString()));

            HMACSHA1 signatureHasher = new HMACSHA1(new ASCIIEncoding().GetBytes(
                $"{ConstantValues.OAuthCustomerSecret}&{ConstantValues.OAuthAccessSecret}"));

            return Convert.ToBase64String(signatureHasher.ComputeHash(new ASCIIEncoding().GetBytes(fullSigData)));
        }

        static string GenerateOAuthHeader(Dictionary<string, string> data)
        {
            return $@"OAuth { string.Join(", ",
                data.Where(kvp => kvp.Key.StartsWith("oauth_"))
                    .Select(kvp => string.Format("{0}=\"{1}\"", 
                        Uri.EscapeDataString(kvp.Key), Uri.EscapeDataString(kvp.Value)))
                    .OrderBy(s => s))}";
        }
    }
}
