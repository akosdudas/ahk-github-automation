using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ahk.GitHub.Monitor.EventHandlers.Lifecycle.Payload;
using Ahk.GitHub.Monitor.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Ahk.GitHub.Monitor.EventHandlers.Lifecycle
{
    public class WorkflowHandler : RepositoryEventBase<WorkflowEventPayload>
    {
        public WorkflowHandler(IGitHubClientFactory gitHubClientFactory, IMemoryCache cache, ILogger logger)
            : base(gitHubClientFactory, cache, logger)
        {
        }

        protected override Task<EventHandlerResult> executeCore(WorkflowEventPayload webhookPayload) => throw new NotImplementedException();
    }
}
