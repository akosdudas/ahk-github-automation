using System.Threading.Tasks;

namespace Ahk.GitHub.Monitor.Services
{
    public interface ILifecycleStore
    {
        public Task StoreRepositoryCreateEvent(string repository, string username);
        public Task StoreBranchCreateEvent(string repository, string username, string branch);
        public Task StoreWorkflowRunEvent(string repository, string username, string conclusion);
        public Task StorePullRequestEvent(string repository, string username, string action, string assignee, string neptun);
    }
}
