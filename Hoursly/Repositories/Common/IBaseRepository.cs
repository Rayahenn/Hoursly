using System;
using System.Collections.Generic;

namespace Hoursly.Repositories.Common
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        TEntity Get(Guid publicId);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(Guid publicId);
        void Commit();
    }
}