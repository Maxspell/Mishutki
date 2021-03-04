using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Mishutki.Domain.Entities;

namespace Mishutki.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostRating> PostRatings { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<BannerPlace> BannerPlaces { get; set; }
    }
}
