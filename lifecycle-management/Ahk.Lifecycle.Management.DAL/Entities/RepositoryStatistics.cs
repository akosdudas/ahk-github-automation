namespace Ahk.Lifecycle.Management.DAL.Entities
{
    public class RepositoryStatistics
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string BranchName { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string[] Assignees { get; set; } = null!;
        public string Neptun { get; set; } = null!;
        public string Conclusion { get; set; } = null!;
        public bool IsGraded { get; set; } = false;
        public int WorkflowRunCount { get; set; }
        public int EventCount { get; set; }
        public string FirstEventType { get; set; } = null!;
        public DateTime FirstEventDate { get; set; }
        public string FirstEventId { get; set; } = null!;
        public string LastEventType { get; set; } = null!;
        public DateTime LastEventDate { get; set; }
        public string LastEventId { get; set; } = null!;
    }
}
