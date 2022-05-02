namespace Ahk.Lifecycle.Management.DAL.Entities
{
    public class RepositoryCreateEvent : LifecycleEvent
    {
        public RepositoryCreateEvent(string id, string repository, string username, DateTime timestamp)
        {
            this.Id = id ?? Guid.NewGuid().ToString();
            this.Repository = repository;
            this.Username = username;
            this.Timestamp = timestamp;
        }

        public override string Id { get; }
        public override string Repository { get; }
        public override string Username { get; }
        public override DateTime Timestamp { get; }
        public override string Type { get; } = nameof(RepositoryCreateEvent);
    }
}
