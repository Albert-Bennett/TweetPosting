using System;

namespace TweetPosingBackend.Controller.Helpers
{
    public static class DateTimeHelper
    {
        public static int TimestampNow()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (int)timeSpan.TotalSeconds;
        }
    }
}