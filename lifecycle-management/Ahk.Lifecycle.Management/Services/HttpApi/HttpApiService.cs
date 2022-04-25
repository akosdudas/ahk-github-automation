using System.Collections;
using System.Threading.Tasks;
using Ahk.Lifecycle.Management.DAL;

namespace Ahk.Lifecycle.Management
{
    public class HttpApiService : IHttpApiService
    {
        private readonly IRepository repo;
        public HttpApiService(IRepository repo) => this.repo = repo;
        public Task<ICollection> GetMany(string repository = "", string username = "", int page = 1, int limit = 10) => repo.GetMany(repository, username, page, limit);
    }
}
