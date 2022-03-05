using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ahk.GitHub.Monitor.EventHandlers.Lifecycle.Response;
using Octokit;

namespace Ahk.GitHub.Monitor.EventHandlers.Lifecycle.Payload
{
    public class WorkflowEventPayload : ActivityPayload
    {
        public string Action { get; protected set; }
        public WorkflowRun WorkflowRun { get; protected set; }
        public Workflow Workflow { get; protected set; }
        public Organization Organization { get; protected set; }
    }
}
