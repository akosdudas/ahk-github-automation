namespace Ahk.Lifecycle.Management.DAL.Entities
{
    public class RepositoryCreateEvent : LifecycleEvent
    {
        public override string EventType { get; set; } = "RepositoryCreateEvent";
    }
}
