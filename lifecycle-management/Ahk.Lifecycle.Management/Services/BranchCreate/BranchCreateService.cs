using System;
using System.Threading.Tasks;
using Ahk.Lifecycle.Management.DAL;
using Ahk.Lifecycle.Management.DAL.Entities;

namespace Ahk.Lifecycle.Management
{
    public class BranchCreateService : IBranchCreateService
    {
        private readonly IRepository repo;

        public BranchCreateService(IRepository repo) => this.repo = repo;

        public Task BranchCreate(BranchCreateEvent data) => repo.Insert(data);
    }
}
