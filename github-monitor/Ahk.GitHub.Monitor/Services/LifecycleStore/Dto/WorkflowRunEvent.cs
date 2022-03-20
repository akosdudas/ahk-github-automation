namespace Ahk.GitHub.Monitor.Services
{
    public class WorkflowRunEvent
    {
        public WorkflowRunEvent(string repository, string username, string conclusion)
        {
            this.Repository = repository;
            this.Username = username;
            this.Conclusion = conclusion;
        }

        public string Repository { get; protected set; }
        public string Username { get; protected set; }

        // neutral, success, skipped, cancelled, timed_out, action_required, failure
        public string Conclusion { get; protected set; }
    }
}