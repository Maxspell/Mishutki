﻿using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mishutki.Domain.Entities;

namespace Mishutki.Domain.Concrete
{
    public class CachedPostRepository : EFRepository<Post>
    {
        private static readonly object CacheLockObject = new object();

        public CachedPostRepository(EFDbContext dbContext)
            : base(dbContext)
        {
        }

        public override IEnumerable<Post> GetAll(
            Expression<Func<Post, bool>> filter = null,
            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null,
            string includeProperties = "")
        {
            string cacheKey = "GetPost";

            var result = HttpRuntime.Cache[cacheKey] as List<Post>;
            if (result == null)
            {
                lock (CacheLockObject)
                {
                    result = HttpRuntime.Cache[cacheKey] as List<Post>;
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
