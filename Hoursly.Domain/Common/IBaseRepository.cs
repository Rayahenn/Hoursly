using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hoursly.Domain.Common
{
    public interface IBaseEntityRepository<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TEntity> GetAsync(Guid publicId, CancellationToken cancellationToken = default);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}