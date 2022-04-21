using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Ahk.Lifecycle.Management
{
    public class BranchCreateEventFunction
    {
        private readonly IBranchCreateService _service;

        public BranchCreateEventFunction(IBranchCreateService service) => _service = service;

        [FunctionName("BranchCreateEventFunction")]
        public async Task Run([QueueTrigger("ahk-branch-create", Connection = "AHK_EventsQueueConnectionString")]BranchCreateEvent data, ILogger log)
        {
            log.LogInformation($"RepositoryCreateEventFunction triggered for Repository='{data.Repository}', Username='{data.Username}', Branch='{data.Branch}'");
            await _service.BranchCreate(data);
        }
    }
}
