using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace Ahk.GitHub.Monitor.EventHandlers.Lifecycle.Response
{
    public class WorkflowRun
    {
        public WorkflowRun()
        {
        }

        public WorkflowRun(string artifactsUrl, string cancelUrl, long checkSuiteId, string checkSuiteUrl, string conclusion, DateTimeOffset createdAt, string @event, string headBranch, Commit headCommit, Repository headRepository, string headSha, string htmlUrl, long id, string jobsUrl, string logsUrl, string name, string nodeId, string previousAttemptUrl, PullRequest pullRequest, Repository repository, string rerunUrl, int runAttempt, int runNumber, DateTimeOffset runStartedAt, string status, DateTimeOffset updatedAt, string url, long workflowId, string workflowUrl)
        {
            this.ArtifactsUrl = artifactsUrl;
            this.CancelUrl = cancelUrl;
            this.CheckSuiteId = checkSuiteId;
            this.CheckSuiteUrl = checkSuiteUrl;
            this.Conclusion = conclusion;
            this.CreatedAt = createdAt;
            this.Event = @event;
            this.HeadBranch = headBranch;
            this.HeadCommit = headCommit;
            this.HeadRepository = headRepository;
            this.HeadSha = headSha;
            this.HtmlUrl = htmlUrl;
            this.Id = id;
            this.JobsUrl = jobsUrl;
            this.LogsUrl = logsUrl;
            this.Name = name;
            this.NodeId = nodeId;
            this.PreviousAttemptUrl = previousAttemptUrl;
            this.PullRequest = pullRequest;
            this.Repository = repository;
            this.RerunUrl = rerunUrl;
            this.RunAttempt = runAttempt;
            this.RunNumber = runNumber;
            this.RunStartedAt = runStartedAt;
            this.Status = status;
            this.UpdatedAt = updatedAt;
            this.Url = url;
            this.WorkflowId = workflowId;
            this.WorkflowUrl = workflowUrl;
        }

        public string ArtifactsUrl { get; protected set; }
        public string CancelUrl { get; protected set; }
        public long CheckSuiteId { get; protected set; }
        public string CheckSuiteUrl { get; protected set; }
        public string Conclusion { get; protected set; }
        public DateTimeOffset CreatedAt { get; protected set; }
        public string Event { get; protected set; }
        public string HeadBranch { get; protected set; }
        public Commit HeadCommit { get; protected set; }
        public Repository HeadRepository { get; protected set; }
        public string HeadSha { get; protected set; }
        public string HtmlUrl { get; protected set; }
        public long Id { get; protected set; }
        public string JobsUrl { get; protected set; }
        public string LogsUrl { get; protected set; }
        public string Name { get; protected set; }
        public string NodeId { get; protected set; }
        public string PreviousAttemptUrl { get; protected set; }
        public PullRequest PullRequest { get; protected set; }
        public Repository Repository { get; protected set; }
        public string RerunUrl { get; protected set; }
        public int RunAttempt { get; protected set; }
        public int RunNumber { get; protected set; }
        public DateTimeOffset RunStartedAt { get; protected set; }
        public string Status { get; protected set; }
        public DateTimeOffset UpdatedAt { get; protected set; }
        public string Url { get; protected set; }
        public long WorkflowId { get; protected set; }
        public string WorkflowUrl { get; protected set; }
    }
}
