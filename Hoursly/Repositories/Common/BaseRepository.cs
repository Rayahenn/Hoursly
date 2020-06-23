using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Hoursly.Common.Decorators;

namespace Hoursly.Repositories.Common
{
    public abstract class BaseRepository<TEntity> where TEntity : class, IUniqueIdentifier
    {
        private readonly HourslyDbContex _dbContex;

        protected BaseRepository(HourslyDbContex dbContex)
        {
            _dbContex = dbContex;
        }

        public List<TEntity> GetAll()
        {
            return _dbContex.Set<TEntity>().AsNoTracking().ToList();
        }

        public TEntity Get(Guid publicId)
        {
            return _dbContex.Set<TEntity>()
                .AsNoTracking()
                .ToList()
                .Single(x => x.PublicId == publicId);
        }

        public void Create(TEntity entity)
        {
            _dbContex.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContex.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Guid publicId)
        {
            var entity = _dbContex.Set<TEntity>().ToList().Single(x => x.PublicId == publicId);
            _dbContex.Set<TEntity>().Remove(entity);
        }

        public void Commit()
        {
            _dbContex.SaveChanges();
        }
    }
}