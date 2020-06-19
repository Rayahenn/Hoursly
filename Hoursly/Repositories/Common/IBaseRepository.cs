using System.Collections.Generic;

namespace Hoursly.Repositories.Common
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();

        void Create(TEntity entity);
    }
}