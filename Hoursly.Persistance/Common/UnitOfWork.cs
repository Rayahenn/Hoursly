using System.Threading;
using System.Threading.Tasks;
using Hoursly.Domain.Common;

namespace Hoursly.Persistance.Common
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly HourslyDbContext _hourslyDbContex;

        internal UnitOfWork(HourslyDbContext hourslyDbContex)
        {
            _hourslyDbContex = hourslyDbContex;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _hourslyDbContex.SaveChangesAsync(cancellationToken);
        }
    }
}