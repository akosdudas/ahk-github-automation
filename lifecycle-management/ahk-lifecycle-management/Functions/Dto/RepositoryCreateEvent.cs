namespace Ahk.Lifecycle.Management
{
    public class RepositoryCreateEvent
    {
        public RepositoryCreateEvent(string repository, string username)
        {
            this.Repository = repository;
            this.Username = username;
        }

        public string Repository { get; protected set; }
        public string Username { get; protected set; }
    }
}
