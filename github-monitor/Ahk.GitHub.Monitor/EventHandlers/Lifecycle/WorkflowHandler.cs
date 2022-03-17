using System;
using System.Threading.Tasks;
using Octokit;
using Ahk.GitHub.Monitor.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Ahk.GitHub.Monitor.EventHandlers
{
    public class WorkflowHandler : RepositoryEventBase<WorkflowEventPayload>
    {
        public const string GitHubWebhookEventName = "workflow_run";
        public WorkflowHandler(IGitHubClientFactory gitHubClientFactory, IMemoryCache cache, ILogger logger)
            : base(gitHubClientFactory, cache, logger)
        {
        }

        protected override async Task<EventHandlerResult> executeCore(WorkflowEventPayload webhookPayload)
        {
            if (webhookPayload.Repository == null)
                return EventHandlerResult.PayloadError("no repository information in webhook payload");

            if (webhookPayload.WorkflowRun == null)
                return EventHandlerResult.PayloadError("no workflow run information in webhook payload");

            if (webhookPayload.Action.Equals("completed", StringComparison.OrdinalIgnoreCase))
                return await processWorkflowRunEvent(webhookPayload);

            return EventHandlerResult.EventNotOfInterest(webhookPayload.Action);
        }

        private async Task<EventHandlerResult> processWorkflowRunEvent(WorkflowEventPayload webhookPayload)
        {

        }
    }
}
