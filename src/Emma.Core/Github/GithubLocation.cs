namespace Emma.Core.Github
{
    public class GithubLocation : IGithubLocation
    {

        public string User { get; }
        public string Repo { get; }
        public string Path { get; }

        public GithubLocation(string user, string repo, string path)
        {
            User = user;
            Repo = repo;
            Path = path;
        }

        public GithubLocation(string user, string repo)
        {
            User = user;
            Repo = repo;
        }
    }

    public interface IGithubLocation
    {
        string User { get; }
        string Repo { get; }
        string Path { get; }
    }

}