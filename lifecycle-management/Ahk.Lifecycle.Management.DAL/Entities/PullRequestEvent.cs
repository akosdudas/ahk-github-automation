namespace Ahk.Lifecycle.Management.DAL.Entities
{
    public class PullRequestEvent : LifecycleEvent
    {
        public PullRequestEvent(string id, string repository, string username, DateTime timestamp, string action, ICollection<string> assignees, string neptun)
        {
            this.Id = id ?? Guid.NewGuid().ToString();
            this.Repository = repository;
            this.Username = username;
            this.Timestamp = timestamp;
            this.Action = action;
            this.Assignees = assignees;
            this.Neptun = neptun;
        }

        public override string Id { get; }
        public override string Repository { get; }
        public override string Username { get; }
        public override DateTime Timestamp { get; }
        public override string Type { get; } = nameof(PullRequestEvent);
        public string Action { get; }
        public ICollection<string> Assignees { get; }
        public string Neptun { get; }
    }
}
