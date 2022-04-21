using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace Ahk.Lifecycle.Management
{
    public interface IHttpApiService
    {
        Task<ICollection> GetMany(string repository = "", string username = "", int page = 1, int limit = 10);
    }
}
