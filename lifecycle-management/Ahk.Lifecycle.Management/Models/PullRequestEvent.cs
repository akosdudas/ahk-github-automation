using System;

namespace Ahk.Lifecycle.Management
{
    public class PullRequestEvent : LifecycleEvent
    {
        public PullRequestEvent()
        {
            this.EventType = "PullRequestEvent";
        }

        public string Action { get; set; }
        public string[] Assignees { get; set; }
        public string Neptun { get; set; }
    }
}
