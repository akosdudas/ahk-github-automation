using System.Threading.Tasks;

namespace Ahk.Lifecycle.Management
{
    public interface IRepositoryCreateService
    {
        Task RepositoryCreate(RepositoryCreateEvent data);
    }
}
