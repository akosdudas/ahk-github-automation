using System;
using System.Threading.Tasks;

namespace Ahk.Lifecycle.Management
{
    public class PullRequestService : IPullRequestService
    {
        private readonly IRepository _repo;

        public PullRequestService(IRepository repo) => _repo = repo;

        public Task PullRequest(PullRequestEvent data) => _repo.Insert(data);
    }
}
