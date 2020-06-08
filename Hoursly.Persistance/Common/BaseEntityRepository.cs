using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hoursly.Domain.Common;

namespace Hoursly.Persistance.Common
{
    internal class BaseEntityRepository<TEntity> : IBaseEntityRepository<TEntity> where TEntity : Entity
    {
        private readonly HourslyDbContext _contex;

        public BaseEntityRepository(HourslyDbContext contex)
        {
            _contex = contex;
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _contex.AddAsync(entity, cancellationToken);
        }

        public Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetAsync(Guid publicId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}