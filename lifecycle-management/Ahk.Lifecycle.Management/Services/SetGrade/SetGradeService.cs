using System.Threading.Tasks;
using Ahk.Lifecycle.Management.DAL;
using Ahk.Lifecycle.Management.DAL.Entities;

namespace Ahk.Lifecycle.Management
{
    public class SetGradeService : ISetGradeService
    {
        private readonly IRepository repo;

        public SetGradeService(IRepository repo) => this.repo = repo;
        public Task SetGrade(SetGradeEvent data) => repo.Insert(data);
    }
}
