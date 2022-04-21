using System;

namespace Ahk.Lifecycle.Management
{
    public class BranchCreateEvent : LifecycleEvent
    {
        public BranchCreateEvent()
        {
            this.EventType = "BranchCreateEvent";
        }

        public string Branch { get; set; }
    }
}
