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
        private readonly IHttpApiService _service;

        public HttpApiFunction(IHttpApiService service) => _service = service;

        [FunctionName("HttpApiFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("HttpApiFunction triggered");

            string Username = req.Query["username"];
            string Repository = req.Query["repository"];
            string Page = req.Query["page"];
            string Limit = req.Query["limit"];

            if (!int.TryParse(Page, out var page))
                page = 1;

            if (!int.TryParse(Limit, out var limit))
                limit = 10;

            var results = await _service.GetMany(Repository, Username, page, limit);

            return new OkObjectResult(results);
        }
    }
}
