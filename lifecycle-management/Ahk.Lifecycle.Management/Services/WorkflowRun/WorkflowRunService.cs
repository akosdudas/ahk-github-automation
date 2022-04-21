using System;
using System.Threading.Tasks;

namespace Ahk.Lifecycle.Management
{
    public class WorkflowRunService : IWorkflowRunService
    {
        private readonly IRepository _repo;

        public WorkflowRunService(IRepository repo) => _repo = repo;

        public Task WorkflowRun(WorkflowRunEvent data) => _repo.Insert(data);
    }
}
