using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.Infrastructure
{
    public class RepositoryBase<T> : IRepository<T> where T : BaseEntity
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

        //public IList<T> FindByExpressionOrdered(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] orderBy)
        //{
        //    var query = SessionScope.Current.Set<T>().Where(filter).OrderBy(orderBy.First());

        //    if (orderBy.Length > 1) { for (int i = 1; i < orderBy.Length; i++) { query = query.ThenBy(orderBy[i]); } }
        //    return query.ToList();
        //}

        public T Get(Expression<Func<T, bool>> where,
            Expression<Func<T, object>> orderBy = null,
            bool isOrderByAsc = false,
            params string[] navigations)
        {
            var set = dbSet.AsQueryable();
            foreach (string nav in navigations) { set = set.Include(nav); }
            set = set.Where(where);
            if (orderBy != null) { set = isOrderByAsc ? set.OrderBy(orderBy) : set.OrderByDescending(orderBy); }
            return set.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(params string[] navigations)
        {
            var set = dbSet.AsQueryable();
            foreach (string nav in navigations) { set = set.Include(nav); }
            return set.AsEnumerable();
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> where,
            Expression<Func<T, object>> orderBy = null,
            bool isOrderByAsc = false,
            params string[] navigations)
        {
            var set = dbSet.AsQueryable();
            foreach (string nav in navigations) { set = set.Include(nav); }
            set = set.Where(where);
            if (orderBy != null) { set = isOrderByAsc ? set.OrderBy(orderBy) : set.OrderByDescending(orderBy); }
            return set.AsEnumerable();
        }
    }
}
