using System.Collections.Generic;
using System.Linq;

namespace Hoursly.Repositories.Common
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        public List<TEntity> GetAll()
        {
            using (var context = new HourslyDbContex())
            {
                return context.Set<TEntity>().ToList();
            }
        }

        public void Create(TEntity entity)
        {
            using (var context = new HourslyDbContex())
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
        }
    }
}