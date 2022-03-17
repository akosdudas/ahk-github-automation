using System;
using System.Threading.Tasks;
using Ahk.GitHub.Monitor.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Octokit;

namespace Ahk.GitHub.Monitor.EventHandlers
{
    public class PullRequestHandler : RepositoryEventBase<PullRequestEventPayload>
    {
        public const string GitHubWebhookEventName = "pull_request";
        public PullRequestHandler(IGitHubClientFactory gitHubClientFactory, IMemoryCache cache, ILogger logger)
            : base(gitHubClientFactory, cache, logger)
        {
        }

        protected override async Task<EventHandlerResult> executeCore(PullRequestEventPayload webhookPayload)
        {
            if (webhookPayload.Repository == null)
                return EventHandlerResult.PayloadError("no repository information in webhook payload");

            if (webhookPayload.PullRequest == null)
                return EventHandlerResult.PayloadError("no pull request information in webhook payload");

            if (webhookPayload.Action.Equals("opened", StringComparison.OrdinalIgnoreCase) ||
                webhookPayload.Action.Equals("assigned", StringComparison.OrdinalIgnoreCase) ||
                webhookPayload.Action.Equals("review_assigned", StringComparison.OrdinalIgnoreCase) ||
                webhookPayload.Action.Equals("closed", StringComparison.OrdinalIgnoreCase))
                return await processPullRequestEvent(webhookPayload);

            return EventHandlerResult.EventNotOfInterest(webhookPayload.Action);
        }

        private async Task<EventHandlerResult> processPullRequestEvent(PullRequestEventPayload webhookPayload)
        {

        }
    }
}
