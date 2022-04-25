using System.Collections;
using System.Threading.Tasks;
using Ahk.Lifecycle.Management.DAL.Entities;

namespace Ahk.Lifecycle.Management.DAL
{
    public interface IRepository
    {
        Task Insert(RepositoryCreateEvent data);
        Task Insert(BranchCreateEvent data);
        Task Insert(PullRequestEvent data);
        Task Insert(WorkflowRunEvent data);
        Task Insert(SetGradeEvent data);
        Task Upsert(RepositoryStatistics data);
        Task<ICollection> GetMany(string repository = "", string username = "", int page = 1, int limit = 10);
    }
}
