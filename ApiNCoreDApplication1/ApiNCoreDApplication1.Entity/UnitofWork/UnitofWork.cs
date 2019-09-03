using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Transactions;
using ApiNCoreDApplication1.Entity.Context;
using ApiNCoreDApplication1.Entity.Repository;

namespace ApiNCoreDApplication1.Entity.UnitofWork
{

    public interface IUnitOfWork : IDisposable
    {

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class;

        ApiNCoreDApplication1Context Context { get; }
        bool Save();
        Task<bool> SaveAsync();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : IDbConnectionProvider
    {
    }

    public class UnitOfWork : IUnitOfWork
    {
        public ApiNCoreDApplication1Context Context { get; }

        private Dictionary<Type, object> _repositoriesAsync;
        private Dictionary<Type, object> _repositories;
        private bool _disposed;

        public UnitOfWork(ApiNCoreDApplication1Context context)
        {
            Context = context;
            _disposed = false;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
                switch (type.ToString())
                {
                    case "ApiNCoreDApplication1.Entity.Account":
                        _repositories[type] = new AccountRepository(Context);
                        break;
                    case "ApiNCoreDApplication1.Entity.User":
                        _repositories[type] = new UserRepository(Context);
                        break;
                }
            return (IRepository<TEntity>)_repositories[type];
        }

        public IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class
        {
            if (_repositoriesAsync == null) _repositoriesAsync = new Dictionary<Type, object>();
            var type = typeof(TEntity);
            if (!_repositoriesAsync.ContainsKey(type))
                switch (type.ToString())
                {
                    case "ApiNCoreDApplication1.Entity.Account":
                        _repositoriesAsync[type] = new AccountRepositoryAsync(Context);
                        break;
                    case "ApiNCoreDApplication1.Entity.User":
                        _repositoriesAsync[type] = new UserRepositoryAsync(Context);
                        break;
                }
            return (IRepositoryAsync<TEntity>)_repositoriesAsync[type];
        }

        public bool Save()
        {
            if (Context.Transaction != null)
                Context.Transaction.Commit();
            return true;
        }
        public async Task<bool> SaveAsync()
        {
            await Task.Run(() =>
            {
                if (Context.Transaction != null)
                    Context.Transaction.Commit();
            });
            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool isDisposing)
        {
            if (!_disposed)
            {
                if (isDisposing)
                {
                    if (Context.Connection != null) Context.Connection.Dispose();
                    if (Context.Transaction != null) Context.Transaction.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
