using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Ahk.Lifecycle.Management
{
    public class SetGradeEventFunction
    {
        private readonly ISetGradeService _service;

        public SetGradeEventFunction(ISetGradeService service) => _service = service;

        [FunctionName("SetGradeEventFunction")]
        public async Task Run([QueueTrigger("ahk-set-grade", Connection = "AHK_EventsQueueConnectionString")]SetGradeEvent data, ILogger log)
        {
            log.LogInformation($"SetGradeEventFunction triggered for Repository='{data.Repository}', Username='{data.Username}'");
            await _service.SetGrade(data);
        }
    }
}
