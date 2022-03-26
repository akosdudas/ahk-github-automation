namespace Octokit
{
    public class WorkflowEventPayload : ActivityPayload
    {
        public WorkflowEventPayload(string action, WorkflowRun workflowRun)
        {
            this.Action = action;
            this.WorkflowRun = workflowRun;
        }

        public string Action { get; }
        public WorkflowRun WorkflowRun { get; }
    }
}
