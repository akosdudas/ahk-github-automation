using System;
using System.ComponentModel.DataAnnotations;

namespace Ahk.Lifecycle.Management
{
    public class RepositoryStatistics
    {
        [Key]
        public string Repository { get; set; }
        public string Username { get; set; }
        public int EventCount { get; set; }
        public string LastEventType { get; set; }
        public DateTime LastEventDate { get; set; }
        public Guid LastEventId { get; set; }
    }
}
