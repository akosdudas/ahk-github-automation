using System.Threading.Tasks;
using Ahk.Lifecycle.Management.DAL.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Ahk.Lifecycle.Management
{
    public class BranchCreateEventFunction
    {
        private readonly IBranchCreateService service;

        public BranchCreateEventFunction(IBranchCreateService service) => this.service = service;

        [FunctionName("BranchCreateEventFunction")]
        public async Task Run([QueueTrigger("ahk-branch-create", Connection = "AHK_EventsQueueConnectionString")]BranchCreateEvent data, ILogger log)
        {
            log.LogInformation("BranchCreateEventFunction triggered for Repository='{Repository}', Username='{Username}', Branch='{Branch}'", data.Repository, data.Username, data.Branch);
            await service.BranchCreate(data);
        }
    }
}
