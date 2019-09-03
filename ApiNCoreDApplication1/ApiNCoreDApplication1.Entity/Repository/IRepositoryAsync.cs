using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiNCoreDApplication1.Entity.Repository
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Get(Func<T, bool> predicate);
        Task<T> GetOne(object id);
        Task<int> Insert(T entity);
        Task Delete(T entity);
        Task Delete(object id);
        Task Update(object id, T entity);
    }
}
