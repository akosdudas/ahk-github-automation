using System;

namespace Ahk.Lifecycle.Management
{
    public class SetGradeEvent : LifecycleEvent
    {
        public SetGradeEvent()
        {
            this.EventType = "SetGradeEvent";
        }
    }
}
