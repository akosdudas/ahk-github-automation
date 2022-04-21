using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Ahk.Lifecycle.Management
{
    public class RepositoryCreateEventFunction
    {
        private readonly IRepositoryCreateService _service;

        public RepositoryCreateEventFunction(IRepositoryCreateService service) => _service = service;

        [FunctionName("RepositoryCreateEventFunction")]
        public async Task Run([QueueTrigger("ahk-repository-create", Connection = "AHK_EventsQueueConnectionString")]RepositoryCreateEvent data, ILogger log)
        {
            log.LogInformation($"RepositoryCreateEventFunction triggered for Repository='{data.Repository}', Username='{data.Username}'");
            await _service.RepositoryCreate(data);
        }
    }
}
