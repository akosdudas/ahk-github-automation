using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Ahk.Lifecycle.Management
{
    public class PullRequestEventFunction
    {
        private readonly IPullRequestService _service;

        public PullRequestEventFunction(IPullRequestService service) => _service = service;

        [FunctionName("PullRequestEventFunction")]
        public async Task Run([QueueTrigger("ahk-pull-request", Connection = "AHK_EventsQueueConnectionString")]PullRequestEvent data, ILogger log)
        {
            log.LogInformation($"RepositoryCreateEventFunction triggered for Repository='{data.Repository}', Username='{data.Username}', Action='{data.Action}', Assignees='{data.Assignees}', Neptun='{data.Neptun}'");
            await _service.PullRequest(data);
        }
    }
}
