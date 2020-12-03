using System;

namespace GithubRepositoryModel
{
    public class Credentials
    {
        public static string AppKey()
        {
            return Environment.GetEnvironmentVariable("EMMA_APP_KEY");
        }
    }
}