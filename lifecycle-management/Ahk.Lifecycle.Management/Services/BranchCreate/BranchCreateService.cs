using System;
using System.Threading.Tasks;

namespace Ahk.Lifecycle.Management
{
    public class BranchCreateService : IBranchCreateService
    {
        private readonly IRepository _repo;

        public BranchCreateService(IRepository repo) => _repo = repo;

        public Task BranchCreate(BranchCreateEvent data) => _repo.Insert(data);
    }
}
