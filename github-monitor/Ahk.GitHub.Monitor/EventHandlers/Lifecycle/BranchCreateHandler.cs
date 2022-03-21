using System;
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
        private readonly ILifecycleStore lifecycleStore;

        public BranchCreateHandler(IGitHubClientFactory gitHubClientFactory, ILifecycleStore lifecycleStore, IMemoryCache cache, ILogger logger)
            : base(gitHubClientFactory, cache, logger)
        {
            this.lifecycleStore = lifecycleStore;
        }

        protected override async Task<EventHandlerResult> executeCore(CreateEventPayload webhookPayload)
        {
            if (webhookPayload.Repository == null)
                return EventHandlerResult.PayloadError("no repository information in webhook payload");

            if (webhookPayload.RefType.Equals(RefType.Branch) && !webhookPayload.Ref.Equals("master", StringComparison.OrdinalIgnoreCase) && !webhookPayload.Ref.Equals("main", StringComparison.OrdinalIgnoreCase))
                return await processBranchCreateEvent(webhookPayload);

            return EventHandlerResult.EventNotOfInterest(webhookPayload.RefType.ToString());
        }

        private async Task<EventHandlerResult> processBranchCreateEvent(CreateEventPayload webhookPayload)
        {
            string repository = webhookPayload.Repository.FullName;
            string username = webhookPayload.Repository.FullName.Split("-")[^1];
            string branch = webhookPayload.Ref;

            await lifecycleStore.StoreBranchCreateEvent(
                repository: repository,
                username: username,
                branch: branch);

            return EventHandlerResult.ActionPerformed("branch create operation done");
        }
    }
}
