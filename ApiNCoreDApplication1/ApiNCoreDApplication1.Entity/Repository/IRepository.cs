using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiNCoreDApplication1.Entity.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Func<T, bool> predicate);
        T GetOne(object id);
        int Insert(T entity);
        void Delete(T entity);
        void Delete(object id);
        void Update(object id, T entity);
    }
}
