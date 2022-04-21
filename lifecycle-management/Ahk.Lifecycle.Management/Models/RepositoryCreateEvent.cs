using System;

namespace Ahk.Lifecycle.Management
{
    public class RepositoryCreateEvent : LifecycleEvent
    {
        public RepositoryCreateEvent()
        {
            this.EventType = "RepositoryCreateEvent";
        }
    }
}
