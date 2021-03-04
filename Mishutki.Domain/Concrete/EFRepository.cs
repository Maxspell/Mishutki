using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mishutki.Domain.Abstract;

namespace Mishutki.Domain.Concrete
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        ///     The db context
        /// </summary>
        internal EFDbContext DbContext;

        /// <summary>
        ///     The db set
        /// </summary>
        internal DbSet<T> DbSet;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EFRepository{T}" /> class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        /// <exception cref="System.ArgumentNullException">dbContext</exception>
        public EFRepository(EFDbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");

            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        #region IRepository<T> Members

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <summary>
        ///     Returns an IQueryable based on the passed-in Expression Database
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Find(Expression<Func<T, bool>> perdicate)
        {
            return DbSet.Where(perdicate);
        }

        /// <summary>
        ///     Gets the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public T GetById(object id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        ///     Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        /// <summary>
        ///     Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(T entity)
        {
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        ///     Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(object id)
        {
            T entityToDelete = DbSet.Find(id);
            DbSet.Remove(entityToDelete);
        }

        #endregion
    }
}
