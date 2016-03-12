using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace PandyIT.Core.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool disposed;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        private readonly DbContext dbContext;

        public UnitOfWork(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.dbContext = context;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            IRepository<TEntity> repo;

            if (repositories.ContainsKey(typeof (TEntity)))
            {
                repo = repositories[typeof (TEntity)] as IRepository<TEntity>;
            }
            else
            {
                repo = new RemoteRepository<TEntity>(this.dbContext);
                repositories.Add(typeof (TEntity), repo);
            }
            return repo;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed || !disposing)
                return;

            this.dbContext.Dispose();

            disposed = true;
        }
    }
}
