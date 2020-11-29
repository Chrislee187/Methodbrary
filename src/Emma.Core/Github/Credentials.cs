using System;

namespace Emma.Core.Github
{
    public class Credentials
    {
        public static string AppKey()
        {
            return Environment.GetEnvironmentVariable("EMMA_APP_KEY");
        }
    }
}