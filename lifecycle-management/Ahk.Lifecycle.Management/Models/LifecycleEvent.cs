using System;
using System.ComponentModel.DataAnnotations;

namespace Ahk.Lifecycle.Management
{
    public abstract class LifecycleEvent
    {
        [Key]
        public Guid LifecycleEventId { get; set; } = Guid.NewGuid();
        public string Repository { get; set; }
        public string Username { get; set; }
        public string EventType { get; set; } = "GenericLifecycleEvent";
        public DateTime Timestamp { get; set; }
    }
}
