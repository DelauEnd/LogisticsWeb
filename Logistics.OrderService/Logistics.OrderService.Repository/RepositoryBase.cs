using Logistics.Entities;
using Logistics.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Logistics.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected LogisticsDbContext RepositoryContext;

        public RepositoryBase(LogisticsDbContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll(bool trackChanges)
            => !trackChanges ?
            RepositoryContext.Set<T>()
                .AsNoTracking() :
            RepositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
             => !trackChanges ?
             RepositoryContext.Set<T>()
                 .Where(expression)
                 .AsNoTracking() :
             RepositoryContext.Set<T>()
                 .Where(expression);

        public IQueryable<T> FromQuery(string query, bool trackChanges)
            => !trackChanges ?
            RepositoryContext.Set<T>()
                .FromSqlRaw(query)
                .AsNoTracking() :
            RepositoryContext.Set<T>()
                .FromSqlRaw(query);

        public Task ExecQuery(string query)
            => RepositoryContext.Database.ExecuteSqlRawAsync(query);

        public void Create(T entity)
            => RepositoryContext.Set<T>().Add(entity);

        public void Delete(T entity)
            => RepositoryContext.Set<T>().Remove(entity);

        public void Update(T entity)
            => RepositoryContext.Set<T>().Update(entity);
    }
}
