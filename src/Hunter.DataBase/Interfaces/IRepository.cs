using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hunter.DataBase.Interfaces
{
    public interface IRepository<T, TKey>
    {
        void Add(T Entity);
        T RetrieveByPK(TKey key);
        IQueryable<T> RetrieveAll();
        IQueryable<T> RetrieveAll(Expression<Func<T, bool>> predicate);
        T RetrieveByQuery(Expression<Func<T, bool>> predicate);
        void Update(T Entity);
        void Delete(T Entity);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
