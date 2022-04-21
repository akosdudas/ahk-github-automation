using System.Threading.Tasks;

namespace Ahk.Lifecycle.Management
{
    public interface IWorkflowRunService
    {
        Task WorkflowRun(WorkflowRunEvent data);
    }
}
