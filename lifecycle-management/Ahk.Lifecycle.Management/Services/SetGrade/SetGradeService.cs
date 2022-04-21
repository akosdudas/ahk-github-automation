using System.Threading.Tasks;

namespace Ahk.Lifecycle.Management
{
    public class SetGradeService : ISetGradeService
    {
        private readonly IRepository _repo;

        public SetGradeService(IRepository repo) => _repo = repo;
        public Task SetGrade(SetGradeEvent data) => _repo.Insert(data);
    }
}
