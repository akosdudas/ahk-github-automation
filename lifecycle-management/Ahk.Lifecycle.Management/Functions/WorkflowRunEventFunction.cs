using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Ahk.Lifecycle.Management
{
    public class WorkflowRunEventFunction
    {
        private readonly IWorkflowRunService _service;

        public WorkflowRunEventFunction(IWorkflowRunService service) => _service = service;

        [FunctionName("WorkflowRunEventFunction")]
        public async Task Run([QueueTrigger("ahk-workflow-run", Connection = "AHK_EventsQueueConnectionString")]WorkflowRunEvent data, ILogger log)
        {
            log.LogInformation($"WorkflowRunEventFunction triggered for Repository='{data.Repository}', Username='{data.Username}', Conclusion='{data.Conclusion}'");
            await _service.WorkflowRun(data);
        }
    }
}
