namespace Octokit
{
    public class WorkflowRun
    {
        public WorkflowRun(string conclusion)
        {
            this.Conclusion = conclusion;
        }

        public string Conclusion { get; }
    }
}
