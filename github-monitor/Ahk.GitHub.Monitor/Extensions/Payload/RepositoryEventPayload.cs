namespace Octokit
{
    public class RepositoryEventPayload : ActivityPayload
    {
        public RepositoryEventPayload(string action) => this.Action = action;

        public string Action { get; }
    }
}
