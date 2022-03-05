using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace Ahk.GitHub.Monitor.EventHandlers.Lifecycle.Payload
{
    public class RepositoryEventPayload : ActivityPayload
    {
        public string Action { get; protected set; }
        public Organization Organization { get; protected set; }
    }
}
