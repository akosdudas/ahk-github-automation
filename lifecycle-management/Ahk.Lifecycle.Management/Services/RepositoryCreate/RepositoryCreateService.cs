using System;
using System.Threading.Tasks;

namespace Ahk.Lifecycle.Management
{
    public class RepositoryCreateService : IRepositoryCreateService
    {
        private readonly IRepository _repo;

        public RepositoryCreateService(IRepository repo) => _repo = repo;
        public Task RepositoryCreate(RepositoryCreateEvent data) => _repo.Insert(data);
    }
}
