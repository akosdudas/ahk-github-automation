using System.Threading.Tasks;

namespace Ahk.GitHub.Monitor.Services
{
    public class LifecycleStoreNoop : ILifecycleStore
    {
        public Task StoreBranchCreateEvent(string repository, string username, string branch) => Task.CompletedTask;
        public Task StorePullRequestEvent(string repository, string username, string action, string assignee, string neptun) => Task.CompletedTask;
        public Task StoreRepositoryCreateEvent(string repository, string username) => Task.CompletedTask;
        public Task StoreWorkflowRunEvent(string repository, string username, string conclusion) => Task.CompletedTask;
    }
}
