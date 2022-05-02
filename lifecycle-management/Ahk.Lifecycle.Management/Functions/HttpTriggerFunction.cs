using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ahk.Lifecycle.Management.DAL;

namespace Ahk.Lifecycle.Management
{
    public class HttpTriggerFunction
    {
        private readonly IRepository service;

        public HttpTriggerFunction(IRepository service) => this.service = service;

        [FunctionName("HttpTriggerFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("HttpTriggerFunction triggered");

            string repository = req.Query["repository"];
            var results = await service.GetRepositories(repository);

            return new OkObjectResult(results);
        }
    }
}
