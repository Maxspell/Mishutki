using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mishutki.Domain.Entities;

namespace Mishutki.Domain.Concrete
{
    public class CachedBannerPlaceRepository : EFRepository<BannerPlace>
    {
        private static readonly object CacheLockObject = new object();

        public CachedBannerPlaceRepository(EFDbContext dbContext)
            : base(dbContext)
        {
        }

        public override IEnumerable<BannerPlace> GetAll(
            Expression<Func<BannerPlace, bool>> filter = null,
            Func<IQueryable<BannerPlace>, IOrderedQueryable<BannerPlace>> orderBy = null,
            string includeProperties = "")
        {
            string cacheKey = "GetBannerPlace";

            var result = HttpRuntime.Cache[cacheKey] as List<BannerPlace>;
            if (result == null)
            {
                lock (CacheLockObject)
                {
                    result = HttpRuntime.Cache[cacheKey] as List<BannerPlace>;
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
