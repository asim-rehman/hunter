using Hunter.DataBase.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hunter.DataBase.Repository
{
    /// <summary>
    /// Base repository, every repo inherits this
    /// </summary>
    /// <typeparam name="T">The entity class</typeparam>
    public class BaseRepository<T> : IRepository<T, Guid> where T : class, new()
    {
        private IHunterDBContext dBContext = null;
        public BaseRepository(IHunterDBContext hunterDBContext)
        {
            dBContext = hunterDBContext;
        }
        public virtual void Add(T Entity)
        {
            dBContext.Set<T>().Add(Entity);
        }
        public virtual void Delete(T Entity)
        {
            dBContext.Set<T>().Remove(Entity);
        }
        public virtual IQueryable<T> RetrieveAll()
        {
            IQueryable<T> query = (from x in dBContext.Set<T>()
                                   select x);
            return query;
        }
        public virtual IQueryable<T> RetrieveAll(System.Linq.Expressions.Expression<System.Func<T, bool>> predicate)
        {
            IQueryable<T> query = (from x in dBContext.Set<T>()
                                   select x).Where(predicate);
            return query;
        }
        public virtual T RetrieveByPK(Guid key)
        {
            return dBContext.Set<T>().Find(key);
        }

        public virtual T RetrieveByQuery(System.Linq.Expressions.Expression<System.Func<T, bool>> predicate)
        {
            return dBContext.Set<T>().Where(predicate).FirstOrDefault();
        }
        public virtual int SaveChanges()
        {
            return dBContext.SaveChanges();
        }
        public virtual Task<int> SaveChangesAsync()
        {
            return dBContext.SaveChangesAsync();
        }
        public virtual void Update(T Entity)
        {
            dBContext.Set<T>().Update(Entity);
        }
    }
}
