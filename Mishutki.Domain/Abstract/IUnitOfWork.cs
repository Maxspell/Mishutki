using System;
using System.Collections.Generic;
using System.Linq;
using Mishutki.Domain.Entities;


namespace Mishutki.Domain.Abstract
{
    public interface IUnitOfWork
    {
        IRepository<Category> Categories { get; }
        IRepository<Post> Posts { get; }
        IRepository<Tag> Tags { get; }
        IRepository<PostRating> PostRatings { get; }
        IRepository<Banner> Banners { get; }
        IRepository<BannerPlace> BannerPlaces { get; }
        void Commit();
    }
}
