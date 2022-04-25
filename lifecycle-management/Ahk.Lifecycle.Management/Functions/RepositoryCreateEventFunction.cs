using System.Threading.Tasks;
using Ahk.Lifecycle.Management.DAL.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Ahk.Lifecycle.Management
{
    public class RepositoryCreateEventFunction
    {
        private readonly IRepositoryCreateService service;

        public RepositoryCreateEventFunction(IRepositoryCreateService service) => this.service = service;

        [FunctionName("RepositoryCreateEventFunction")]
        public async Task Run([QueueTrigger("ahk-repository-create", Connection = "AHK_EventsQueueConnectionString")]RepositoryCreateEvent data, ILogger log)
        {
            log.LogInformation("RepositoryCreateEventFunction triggered for Repository='{Repository}', Username='{Username}'", data.Repository, data.Username);
            await service.RepositoryCreate(data);
        }
    }
}
