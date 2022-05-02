namespace Ahk.Lifecycle.Management.DAL.Entities
{
    public class WorkflowRunEvent : LifecycleEvent
    {
        public WorkflowRunEvent(string id, string repository, string username, DateTime timestamp, string conclusion)
        {
            this.Id = id ?? Guid.NewGuid().ToString();
            this.Repository = repository;
            this.Username = username;
            this.Timestamp = timestamp;
            this.Conclusion = conclusion;
        }

        public override string Id { get; }
        public override string Repository { get; }
        public override string Username { get; }
        public override DateTime Timestamp { get; }
        public override string Type { get; } = nameof(WorkflowRunEvent);
        public string Conclusion { get; }
    }
}
