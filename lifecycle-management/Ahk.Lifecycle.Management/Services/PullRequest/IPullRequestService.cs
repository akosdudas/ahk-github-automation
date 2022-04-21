using System.Threading.Tasks;

namespace Ahk.Lifecycle.Management
{
    public interface IPullRequestService
    {
        Task PullRequest(PullRequestEvent data);
    }
}
