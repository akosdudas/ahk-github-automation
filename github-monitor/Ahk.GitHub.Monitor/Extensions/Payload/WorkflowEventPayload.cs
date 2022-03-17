namespace Octokit
{
    public class WorkflowEventPayload : ActivityPayload
    {
        public string Action { get; protected set; }
        public WorkflowRun WorkflowRun { get; protected set; }
        public Workflow Workflow { get; protected set; }
        public Organization Organization { get; protected set; }
    }
}
