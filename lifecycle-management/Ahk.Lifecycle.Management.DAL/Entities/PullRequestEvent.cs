namespace Ahk.Lifecycle.Management.DAL.Entities
{
    public class PullRequestEvent : LifecycleEvent
    {
        public string Action { get; set; } = null!;
        public string[] Assignees { get; set; } = null!;
        public string Neptun { get; set; } = null!;
        public override string EventType { get; set; } = "PullRequestEvent";
    }
}
