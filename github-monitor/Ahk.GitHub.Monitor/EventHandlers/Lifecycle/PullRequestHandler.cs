using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ahk.GitHub.Monitor.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Octokit;

namespace Ahk.GitHub.Monitor.EventHandlers.Lifecycle
{
    public class PullRequestHandler : RepositoryEventBase<PullRequestEventPayload>
    {
        public PullRequestHandler(IGitHubClientFactory gitHubClientFactory, IMemoryCache cache, ILogger logger)
            : base(gitHubClientFactory, cache, logger)
        {
        }

        protected override Task<EventHandlerResult> executeCore(PullRequestEventPayload webhookPayload) => throw new NotImplementedException();
    }
}
