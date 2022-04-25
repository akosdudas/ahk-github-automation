using System;
using System.Threading.Tasks;
using Ahk.Lifecycle.Management.DAL;
using Ahk.Lifecycle.Management.DAL.Entities;

namespace Ahk.Lifecycle.Management
{
    public class RepositoryCreateService : IRepositoryCreateService
    {
        private readonly IRepository repo;

        public RepositoryCreateService(IRepository repo) => this.repo = repo;
        public Task RepositoryCreate(RepositoryCreateEvent data) => repo.Insert(data);
    }
}
