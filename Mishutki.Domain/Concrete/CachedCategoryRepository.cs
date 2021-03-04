using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mishutki.Domain.Entities;

namespace Mishutki.Domain.Concrete
{
    public class CachedCategoryRepository : EFRepository<Category>
    {
        private static readonly object CacheLockObject = new object();

        public CachedCategoryRepository(EFDbContext dbContext)
            : base(dbContext)
        {
        }

        public override IEnumerable<Category> GetAll(
            Expression<Func<Category, bool>> filter = null,
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null,
            string includeProperties = "")
        {
            string cacheKey = "GetCategory";

            var result = HttpRuntime.Cache[cacheKey] as List<Category>;
            if (result == null)
            {
                lock (CacheLockObject)
                {
                    result = HttpRuntime.Cache[cacheKey] as List<Category>;
                    if (result == null)
                    {
                        result = base.GetAll(filter, orderBy, includeProperties).ToList();
                        HttpRuntime.Cache.Insert(cacheKey, result, null,
                            DateTime.Now.AddSeconds(600), TimeSpan.Zero);
                    }
                }
            }
            return result;
        }
    }
}
