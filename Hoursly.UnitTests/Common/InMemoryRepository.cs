using System;
using System.Collections.Generic;
using System.Linq;
using Hoursly.Common;
using Hoursly.Repositories.Common;

namespace Hoursly.UnitTests.Common
{
    public class InMemoryRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IUniqueIdentifier
    {
        private readonly IList<TEntity> _tempEntities = new List<TEntity>();
        private IList<TEntity> _entities = new List<TEntity>();

        public void Commit()
        {
            _entities = _tempEntities;
        }

        public void Create(TEntity entity)
        {
            _tempEntities.Add(entity);
        }

        public void Delete(Guid publicId)
        {
            var entityToRemove = _tempEntities.Single(c => c.PublicId == publicId);
            _tempEntities.Remove(entityToRemove);
        }

        public TEntity Get(Guid publicId)
        {
            return _entities.Single(c => c.PublicId == publicId);
        }

        public List<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        public void Update(TEntity entity)
        {
            var entityToUpdate = _tempEntities.Single(c => c.PublicId == entity.PublicId);
            _tempEntities.Remove(entityToUpdate);
            _tempEntities.Add(entity);
        }
    }
}