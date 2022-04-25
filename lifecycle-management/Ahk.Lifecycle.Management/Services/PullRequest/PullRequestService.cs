using System;
using System.Threading.Tasks;
using Ahk.Lifecycle.Management.DAL;
using Ahk.Lifecycle.Management.DAL.Entities;

namespace Ahk.Lifecycle.Management
{
    public class PullRequestService : IPullRequestService
    {
        private readonly IRepository repo;

        public PullRequestService(IRepository repo) => this.repo = repo;

        public Task PullRequest(PullRequestEvent data) => repo.Insert(data);
    }
}
