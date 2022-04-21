using System.Threading.Tasks;

namespace Ahk.Lifecycle.Management
{
    public interface IBranchCreateService
    {
        Task BranchCreate(BranchCreateEvent data);
    }
}
