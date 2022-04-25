using System;
using System.Threading.Tasks;
using Ahk.Lifecycle.Management.DAL;
using Ahk.Lifecycle.Management.DAL.Entities;

namespace Ahk.Lifecycle.Management
{
    public class WorkflowRunService : IWorkflowRunService
    {
        private readonly IRepository repo;

        public WorkflowRunService(IRepository repo) => this.repo = repo;

        public Task WorkflowRun(WorkflowRunEvent data) => repo.Insert(data);
    }
}
