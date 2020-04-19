using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : BaseEntity
    {
        protected ApplicationDbContext dbContext;
        private readonly DbSet<T> dbSet;

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T item in objects) { dbSet.Remove(item); }
        }

        public T Get(long id, params string[] navigations)
        {
            var set = dbSet.AsQueryable();
            foreach (string nav in navigations) { set = set.Include(nav); }
            return set.FirstOrDefault(x => x.id == id);
        }

        public T Get(Expression<Func<T, bool>> where, params string[] navigations)
        {
            var set = dbSet.AsQueryable();
            foreach (string nav in navigations) { set = set.Include(nav); }
            return set.FirstOrDefault(where);
        }

        public IEnumerable<T> GetAll(params string[] navigations)
        {
            var set = dbSet.AsQueryable();
            foreach (string nav in navigations) { set = set.Include(nav); }
            return set.AsEnumerable();
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> where, params string[] navigations)
        {
            var set = dbSet.AsQueryable();
            foreach (string nav in navigations) { set = set.Include(nav); }
            return set.Where(where).AsEnumerable();
        }
    }
}
