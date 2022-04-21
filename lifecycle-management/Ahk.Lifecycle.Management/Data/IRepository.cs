using System.Collections;
using System.Threading.Tasks;

namespace Ahk.Lifecycle.Management
{
    public interface IRepository
    {
        Task Insert(LifecycleEvent data);
        Task Insert(RepositoryStatistics data);
        Task Update(RepositoryStatistics data);
        Task<ICollection> GetOne(string repo);
        Task<ICollection> GetMany(string repository = "", string username = "", int page = 1, int limit = 10);
    }
}
