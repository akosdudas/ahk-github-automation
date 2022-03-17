namespace Octokit
{
    public class RepositoryEventPayload : ActivityPayload
    {
        public string Action { get; protected set; }
        public Organization Organization { get; protected set; }
    }
}
