namespace Ahk.Lifecycle.Management.DAL.Entities
{
    public abstract class LifecycleEvent
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Repository { get; set; } = null!;
        public string Username { get; set; } = null!;
        public abstract string EventType { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
