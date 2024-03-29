using System;
using System.Threading.Tasks;
using Ahk.GradeManagement.Data.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Ahk.GradeManagement.StatusTracking
{
    public class PullRequestEventFunction
    {
        private readonly IStatusTrackingService service;

        public PullRequestEventFunction(IStatusTrackingService service) => this.service = service;

        [FunctionName("PullRequestEventFunction")]
        public async Task Run([QueueTrigger("ahkstatustrackingpullrequest", Connection = "AHK_EventsQueueConnectionString")] PullRequestEvent data, ILogger log)
        {
            log.LogInformation("PullRequestEventFunction triggered for Repository='{Repository}', Action='{Action}', Neptun='{Neptun}'", data.Repository, data.Action, data.Neptun);

            if (string.IsNullOrEmpty(data.Repository) || data.Number == 0)
            {
                log.LogWarning("PullRequestEventFunction triggered for Repository='{Repository}', Action='{Action}', Neptun='{Neptun}'", data.Repository, data.Action, data.Neptun);
                return;
            }

            try
            {
                await service.InsertNewEvent(data);
                log.LogInformation("PullRequestEventFunction completed for Repository='{Repository}', Action='{Action}', Neptun='{Neptun}'", data.Repository, data.Action, data.Neptun);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "PullRequestEventFunction failed for Repository='{Repository}', Action='{Action}', Neptun='{Neptun}'", data.Repository, data.Action, data.Neptun);
                throw;
            }
        }
    }
}
