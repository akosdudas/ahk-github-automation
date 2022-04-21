using System.Collections;
using System.Threading.Tasks;

namespace Ahk.Lifecycle.Management
{
    public class HttpApiService : IHttpApiService
    {
        private readonly IRepository _repo;
        public HttpApiService(IRepository repo) => _repo = repo;
        public Task<ICollection> GetMany(string repository = "", string username = "", int page = 1, int limit = 10) => _repo.GetMany(repository, username, page, limit);
    }
}
