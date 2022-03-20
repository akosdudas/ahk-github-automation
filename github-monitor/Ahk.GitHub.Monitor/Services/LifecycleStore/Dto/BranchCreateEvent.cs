namespace Ahk.GitHub.Monitor.Services
{
    public class BranchCreateEvent
    {
        public BranchCreateEvent(string repository, string username, string branch)
        {
            this.Repository = repository;
            this.Username = username;
            this.Branch = branch;
        }

        public string Repository { get; protected set; }
        public string Username { get; protected set; }
        public string Branch { get; protected set; }
    }
}
