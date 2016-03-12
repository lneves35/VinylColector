using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;

namespace PandyIT.Core.Repository
{
    public class RemoteRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext dbContext;

        public RemoteRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            this.dbContext = dbContext;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return this.dbContext.Set<TEntity>().AsExpandable().Where(predicate);
        }

        public TEntity Add(TEntity entity)
        {
            var ret = this.dbContext.Set<TEntity>().Add(entity);
            this.dbContext.SaveChanges();
            return ret;
        }

        public void Remove(TEntity entity)
        {
            if (this.dbContext.Entry(entity).State == EntityState.Detached)
            {
                this.dbContext.Set<TEntity>().Attach(entity);
            }
            this.dbContext.Set<TEntity>().Remove(entity);
            this.dbContext.SaveChanges();
        }

        public TEntity Update(TEntity entity)
        {
            this.dbContext.Set<TEntity>().Attach(entity);
            this.dbContext.Entry(entity).State = EntityState.Modified;
            this.dbContext.SaveChanges();
            return entity;
        }
    }
}
