namespace GithubRepositoryModel
{
    public interface IGhBranch
    {
        string Name { get; }
        GhFolder Root { get; }

        // TODO: Add remaining members from Branch

    }
}