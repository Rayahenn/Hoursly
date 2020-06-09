using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hoursly.Domain.Common;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _contex.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetAsync(Guid publicId, CancellationToken cancellationToken = default)
        {
            return await _contex.Set<TEntity>().SingleAsync(c => c.PublicId == publicId, cancellationToken);
        }
    }
}