using System;

namespace Ahk.Lifecycle.Management
{
    public class WorkflowRunEvent : LifecycleEvent
    {
        public WorkflowRunEvent()
        {
            this.EventType = "WorkflowRunEvent";
        }

        public string Conclusion { get; set; }
    }
}
