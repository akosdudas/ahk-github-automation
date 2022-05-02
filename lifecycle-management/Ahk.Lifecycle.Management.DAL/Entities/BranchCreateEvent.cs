namespace Ahk.Lifecycle.Management.DAL.Entities
{
    public class BranchCreateEvent : LifecycleEvent
    {
        public BranchCreateEvent(string id, string repository, string username, DateTime timestamp, string branch)
        {
            this.Id = id ?? Guid.NewGuid().ToString();
            this.Repository = repository;
            this.Username = username;
            this.Timestamp = timestamp;
            this.Branch = branch;
        }

        public override string Id { get; }
        public override string Repository { get; }
        public override string Username { get; }
        public override DateTime Timestamp { get; }
        public override string Type { get; } = nameof(BranchCreateEvent);
        public string Branch { get; }
    }
}
