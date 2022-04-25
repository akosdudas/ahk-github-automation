namespace Ahk.Lifecycle.Management.DAL.Entities
{
    public class RepositoryStatistics
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public int EventCount { get; set; }
        public string LastEventType { get; set; } = null!;
        public DateTime LastEventDate { get; set; }
        public string LastEventId { get; set; } = null!;
    }
}
