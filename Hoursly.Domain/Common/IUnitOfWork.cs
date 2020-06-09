using System.Threading;
using System.Threading.Tasks;

namespace Hoursly.Domain.Common
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}