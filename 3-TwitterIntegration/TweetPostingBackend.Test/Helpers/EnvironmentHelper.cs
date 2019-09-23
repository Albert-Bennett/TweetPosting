using System;

namespace TweetPostingBackend.Test.Helpers
{
    public static class EnvironmentHelper
    {
        static bool alreadySetup = false;
        public static void SetUpEnvironmentVariables()
        {
            if (!alreadySetup)
            {
                alreadySetup =true;

                Environment.SetEnvironmentVariable("OAUTH_VERSION", "1.0");
                Environment.SetEnvironmentVariable("OAUTH_TOKEN", "747585394699534336-fqWeiEF0MNTfxwWQe1pdNXqtFZa3pCO");
                Environment.SetEnvironmentVariable("OAUTH_CUSTOMER_KEY", "ymgqpnrmNC9h4ZH729t9Nqiwe");
                Environment.SetEnvironmentVariable("SIGNATURE_METHOD", "HMAC-SHA1");
                Environment.SetEnvironmentVariable("OAUTH_CUSTOMER_SECRET", "pT3l6RhwrP9UtEFQLoJ4tOIbh1YwPFpcep5nq0vHuucPScFXLN");
                Environment.SetEnvironmentVariable("OAUTH_ACCESS_SECRET", "re21rWklCHA3ZwQuTJIQpCarusjSlASHnM1mmuwIcEFm7");

                Environment.SetEnvironmentVariable("TWITTER_POST_URL", "https://api.twitter.com/1.1/statuses/update.json");
                Environment.SetEnvironmentVariable("TWITTER_USER_AGENT", "TweetAuthAppDemo1234");
                Environment.SetEnvironmentVariable("TWITTER_HOST", "api.twitter.com");
            }
        }
    }
}
