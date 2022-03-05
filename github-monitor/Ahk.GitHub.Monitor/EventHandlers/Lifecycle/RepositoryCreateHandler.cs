using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ahk.GitHub.Monitor.EventHandlers.Lifecycle.Payload;
using Ahk.GitHub.Monitor.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Octokit;
using Octokit.Internal;

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
                return await processRepositoryCreation(webhookPayload);

            return EventHandlerResult.EventNotOfInterest(webhookPayload.Action);
        }

        private async Task<EventHandlerResult> processRepositoryCreation(RepositoryEventPayload webhookPayload)
        {
            // todo: implementálni
            throw new NotImplementedException();
        }
    }
}
