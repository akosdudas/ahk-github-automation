using System.Threading.Tasks;

namespace Ahk.Lifecycle.Management
{
    public interface ISetGradeService
    {
        Task SetGrade(SetGradeEvent data);
    }
}
