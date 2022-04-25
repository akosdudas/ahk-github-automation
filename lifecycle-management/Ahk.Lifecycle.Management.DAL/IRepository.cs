using System.Collections;
using System.Threading.Tasks;
using Ahk.Lifecycle.Management.DAL.Entities;

namespace Ahk.Lifecycle.Management.DAL
{
    public interface IRepository
    {
        Task Insert(LifecycleEvent data);
        Task Upsert(RepositoryStatistics data);
        Task<ICollection> GetOne(string repo);
        Task<ICollection> GetMany(string repository = "", string username = "", int page = 1, int limit = 10);
    }
}
