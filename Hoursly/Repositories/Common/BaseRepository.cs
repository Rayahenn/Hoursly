using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Hoursly.Common.Decorators;

namespace Hoursly.Repositories.Common
{
    public abstract class BaseRepository<TEntity> where TEntity : class, IUniqueIdentifier
    {
        protected readonly HourslyDbContex DbContex;

        protected BaseRepository(HourslyDbContex dbContex)
        {
            DbContex = dbContex;
        }

        public List<TEntity> GetAll()
        {
            return DbContex.Set<TEntity>().AsNoTracking().ToList();
        }

        public TEntity Get(Guid publicId)
        {
            return DbContex.Set<TEntity>()
                .AsNoTracking()
                .ToList()
                .Single(x => x.PublicId == publicId);
        }

        public void Create(TEntity entity)
        {
            DbContex.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            DbContex.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Guid publicId)
        {
            var entity = DbContex.Set<TEntity>().ToList().Single(x => x.PublicId == publicId);
            DbContex.Set<TEntity>().Remove(entity);
        }

        public void Commit()
        {
            DbContex.SaveChanges();
        }
    }
}