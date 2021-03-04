using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mishutki.Domain.Entities;

namespace Mishutki.Domain.Concrete
{
    public class CachedTagRepository : EFRepository<Tag>
    {
        private static readonly object CacheLockObject = new object();

        public CachedTagRepository(EFDbContext dbContext) : base(dbContext)
        {
        }

        public override IEnumerable<Tag> GetAll(
            Expression<Func<Tag, bool>> filter = null,
            Func<IQueryable<Tag>, IOrderedQueryable<Tag>> orderBy = null,
            string includeProperties = "")
        {
            string cacheKey = "GetTags";

            var result = HttpRuntime.Cache[cacheKey] as List<Tag>;
            if (result == null)
            {
                lock (CacheLockObject)
                {
                    result = HttpRuntime.Cache[cacheKey] as List<Tag>;
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
