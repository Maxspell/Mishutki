using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Mishutki.Domain.Abstract
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        IQueryable<T> Find(Expression<Func<T, bool>> perdicate);
        T GetById(object id);
        void Add(T entity);
        void Update(T entity);
        void Delete(object id);
    }
}
