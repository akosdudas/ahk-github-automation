using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Ahk.Lifecycle.Management.Startup))]

namespace Ahk.Lifecycle.Management
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<CosmosDbContext>();
            builder.Services.AddScoped<IHttpApiService, HttpApiService>();
            builder.Services.AddScoped<IRepository, CosmosDbRepository>();
            builder.Services.AddScoped<IRepositoryCreateService, RepositoryCreateService>();
            builder.Services.AddScoped<IBranchCreateService, BranchCreateService>();
            builder.Services.AddScoped<IPullRequestService, PullRequestService>();
            builder.Services.AddScoped<IWorkflowRunService, WorkflowRunService>();
            builder.Services.AddScoped<ISetGradeService, SetGradeService>();
        }
    }
}
