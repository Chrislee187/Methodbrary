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

        public string ToUrl()
        {
            var url = $@"https:\\github.com\";
            url = BuildPath(url);
            return url;
        }

        private string BuildPath(string url)
        {
            url += $"{User}\\{Repo}";

            if (!string.IsNullOrEmpty(Path))
            {
                url += $"\\{Path}";
            }

            return url;
        }

        public override string ToString() => BuildPath("");
    }

    public interface IGithubLocation
    {
        string User { get; }
        string Repo { get; }
        string Path { get; }
    }

}