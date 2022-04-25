namespace Ahk.Lifecycle.Management.DAL.Entities
{
    public class BranchCreateEvent : LifecycleEvent
    {
        public string Branch { get; set; } = null!;
        public override string EventType { get; set; } = "BranchCreateEvent";
    }
}
