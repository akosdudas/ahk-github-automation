using System.Threading.Tasks;
using Ahk.Lifecycle.Management.DAL.Entities;

namespace Ahk.Lifecycle.Management
{
    public interface IBranchCreateService
    {
        Task BranchCreate(BranchCreateEvent data);
    }
}
