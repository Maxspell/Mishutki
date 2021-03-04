using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mishutki.Domain.Entities;

namespace Mishutki.Domain.Concrete
{
    public class CachedBannerRepository : EFRepository<Banner>
    {
        private static readonly object CacheLockObject = new object();

        public CachedBannerRepository(EFDbContext dbContext)
            : base(dbContext)
        {
        }

        public override IEnumerable<Banner> GetAll(
            Expression<Func<Banner, bool>> filter = null,
            Func<IQueryable<Banner>, IOrderedQueryable<Banner>> orderBy = null,
            string includeProperties = "")
        {
            string cacheKey = "GetBanner";

            var result = HttpRuntime.Cache[cacheKey] as List<Banner>;
            if (result == null)
            {
                lock (CacheLockObject)
                {
                    result = HttpRuntime.Cache[cacheKey] as List<Banner>;
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
