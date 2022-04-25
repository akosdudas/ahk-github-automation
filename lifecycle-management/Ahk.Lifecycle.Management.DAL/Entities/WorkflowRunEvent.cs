namespace Ahk.Lifecycle.Management.DAL.Entities
{
    public class WorkflowRunEvent : LifecycleEvent
    {
        public string Conclusion { get; set; } = null!;
        public override string EventType { get; set; } = "WorkflowRunEvent";
    }
}
