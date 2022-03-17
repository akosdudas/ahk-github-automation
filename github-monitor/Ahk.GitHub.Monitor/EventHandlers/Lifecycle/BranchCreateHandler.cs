using System.Threading.Tasks;
using Ahk.GitHub.Monitor.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Octokit;

namespace Ahk.GitHub.Monitor.EventHandlers
{
    public class BranchCreateHandler : RepositoryEventBase<CreateEventPayload>
    {
        public const string GitHubWebhookEventName = "create";
        public BranchCreateHandler(IGitHubClientFactory gitHubClientFactory, IMemoryCache cache, ILogger logger)
            : base(gitHubClientFactory, cache, logger)
        {
        }

        protected override async Task<EventHandlerResult> executeCore(CreateEventPayload webhookPayload)
        {
            if (webhookPayload.Repository == null)
                return EventHandlerResult.PayloadError("no repository information in webhook payload");

            if (webhookPayload.RefType.Equals(RefType.Branch))
                return await processBranchCreateEvent(webhookPayload);

            return EventHandlerResult.EventNotOfInterest(webhookPayload.RefType.ToString());
        }

        private async Task<EventHandlerResult> processBranchCreateEvent(CreateEventPayload webhookPayload)
        {

        }
    }
}
