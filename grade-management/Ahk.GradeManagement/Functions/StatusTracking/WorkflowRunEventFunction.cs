using System;
using System.Threading.Tasks;
using Ahk.GradeManagement.Data.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Ahk.GradeManagement.StatusTracking
{
    public class WorkflowRunEventFunction
    {
        private readonly IStatusTrackingService service;

        public WorkflowRunEventFunction(IStatusTrackingService service) => this.service = service;

        [FunctionName("WorkflowRunEventFunction")]
        public async Task Run([QueueTrigger("ahkstatustrackingworkflowrun", Connection = "AHK_EventsQueueConnectionString")] WorkflowRunEvent data, ILogger log)
        {
            log.LogInformation("WorkflowRunEventFunction triggered for Repository='{Repository}', Conclusion='{Conclusion}'", data.Repository, data.Conclusion);

            if (string.IsNullOrEmpty(data.Repository))
            {
                log.LogWarning("WorkflowRunEventFunction missing data for Repository='{Repository}', Conclusion='{Conclusion}'", data.Repository, data.Conclusion);
                return;
            }

            try
            {
                await service.InsertNewEvent(data);
                log.LogInformation("WorkflowRunEventFunction completed for Repository='{Repository}', Conclusion='{Conclusion}'", data.Repository, data.Conclusion);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "WorkflowRunEventFunction failed for Repository='{Repository}', Conclusion='{Conclusion}'", data.Repository, data.Conclusion);
                throw;
            }
        }
    }
}
