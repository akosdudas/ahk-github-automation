using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Ahk.Lifecycle.Management
{
    public class HttpApiFunction
    {
        private readonly IHttpApiService service;

        public HttpApiFunction(IHttpApiService service) => this.service = service;

        [FunctionName("HttpApiFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("HttpApiFunction triggered");

            string username = req.Query["username"];
            string repository = req.Query["repository"];
            string page1 = req.Query["page"];
            string limit1 = req.Query["limit"];

            if (!int.TryParse(page1, out var page))
                page = 1;

            if (!int.TryParse(limit1, out var limit))
                limit = 10;

            var results = await service.GetMany(repository, username, page, limit);

            return new OkObjectResult(results);
        }
    }
}
