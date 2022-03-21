namespace Ahk.Lifecycle.Management
{
    public class PullRequestEvent
    {
        public PullRequestEvent(string repository, string username, string action, string assignee, string neptun)
        {
            this.Repository = repository;
            this.Username = username;
            this.Action = action;
            this.Assignee = assignee;
            this.Neptun = neptun;
        }

        public string Repository { get; protected set; }
        public string Username { get; protected set; }
        public string Action { get; protected set; }
        public string Assignee { get; protected set; }
        public string Neptun { get; protected set; }
    }
}
