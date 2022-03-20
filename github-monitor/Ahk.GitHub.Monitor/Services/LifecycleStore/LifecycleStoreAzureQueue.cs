using System.Threading.Tasks;
using Azure.Storage.Queues;
using Microsoft.Extensions.Azure;

namespace Ahk.GitHub.Monitor.Services
{
    public class LifecycleStoreAzureQueue : ILifecycleStore
    {
        public const string QueueClientName = "ahkevents";
        public const string QueueNameRepositoryCreate = "ahk-repository-create";
        public const string QueueNameBranchCreate = "ahk-branch-create";
        public const string QueueNameWorkflowRun = "ahk-workflow-run";
        public const string QueueNamePullRequest = "ahk-pull-request";

        private readonly QueueWithCreateIfNotExists queueRepositoryCreate;
        private readonly QueueWithCreateIfNotExists queueBranchCreate;
        private readonly QueueWithCreateIfNotExists queueWorkflowRun;
        private readonly QueueWithCreateIfNotExists queuePullRequest;

        public LifecycleStoreAzureQueue(IAzureClientFactory<QueueServiceClient> clientFactory)
        {
            var queueService = clientFactory.CreateClient(QueueClientName);
            this.queueRepositoryCreate = new QueueWithCreateIfNotExists(queueService, QueueNameRepositoryCreate);
            this.queueBranchCreate = new QueueWithCreateIfNotExists(queueService, QueueNameBranchCreate);
            this.queueWorkflowRun = new QueueWithCreateIfNotExists(queueService, QueueNameWorkflowRun);
            this.queuePullRequest = new QueueWithCreateIfNotExists(queueService, QueueNamePullRequest);
        }

        public Task StoreRepositoryCreateEvent(string repository, string username)
        {
            var e = new RepositoryCreateEvent(repository, username);
            return queueRepositoryCreate.Send(e);
        }

        public Task StoreBranchCreateEvent(string repository, string username, string branch)
        {
            var e = new BranchCreateEvent(repository, username, branch);
            return queueBranchCreate.Send(e);
        }

        public Task StoreWorkflowRunEvent(string repository, string username, string conclusion)
        {
            var e = new WorkflowRunEvent(repository, username, conclusion);
            return queueWorkflowRun.Send(e);
        }

        public Task StorePullRequestEvent(string repository, string username, string action, string assignee, string neptun)
        {
            var e = new PullRequestEvent(repository, username, action, assignee, neptun);
            return queuePullRequest.Send(e);
        }
    }
}
