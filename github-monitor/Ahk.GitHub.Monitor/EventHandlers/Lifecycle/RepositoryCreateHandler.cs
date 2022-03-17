using System;
using System.Threading.Tasks;
using Octokit;

namespace Ahk.GitHub.Monitor.EventHandlers
{
    public class RepositoryCreateHandler : RepositoryEventBase<RepositoryEventPayload>
    {
        public const string GitHubWebhookEventName = "repository";

        public RepositoryCreateHandler(Services.IGitHubClientFactory gitHubClientFactory, Microsoft.Extensions.Caching.Memory.IMemoryCache cache, Microsoft.Extensions.Logging.ILogger logger)
            : base(gitHubClientFactory, cache, logger)
        {
        }

        protected override async Task<EventHandlerResult> executeCore(RepositoryEventPayload webhookPayload)
        {
            if (webhookPayload.Repository == null)
                return EventHandlerResult.PayloadError("no repository information in webhook payload");

            if (webhookPayload.Action.Equals("created", StringComparison.OrdinalIgnoreCase))
                return await processRepositoryCreateEvent(webhookPayload);

            return EventHandlerResult.EventNotOfInterest(webhookPayload.Action);
        }

        private async Task<EventHandlerResult> processRepositoryCreateEvent(RepositoryEventPayload webhookPayload)
        {

        }
    }
}
