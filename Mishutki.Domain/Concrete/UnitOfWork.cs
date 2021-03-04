using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Mishutki.Domain.Entities;
using Mishutki.Domain.Abstract;

namespace Mishutki.Domain.Concrete
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EFDbContext _context;
        private IRepository<Category> categoryRepository;
        private IRepository<Post> postRepository;
        private IRepository<Tag> tagRepository;
        private IRepository<PostRating> postRatingRepository;
        private IRepository<Banner> bannerRepository;
        private IRepository<BannerPlace> bannerPlaceRepository;

        public UnitOfWork()
        {
            _context = new EFDbContext();
        }

        public UnitOfWork(EFDbContext context)
        {
            _context = context;
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new CachedCategoryRepository(_context);
                }
                return categoryRepository;
            }
        }

        public IRepository<Post> Posts
        {
            get
            {
                if (this.postRepository == null)
                {
                    this.postRepository = new EFRepository<Post>(_context);
                }
                return postRepository;
            }
        }

        public IRepository<Tag> Tags
        {
            get
            {
                if (this.tagRepository == null)
                {
                    this.tagRepository = new CachedTagRepository(_context);
                }
                return tagRepository;
            }
        }

        public IRepository<PostRating> PostRatings
        {
            get
            {
                if (this.postRatingRepository == null)
                {
                    this.postRatingRepository = new EFRepository<PostRating>(_context);
                }
                return postRatingRepository;
            }
        }

        public IRepository<Banner> Banners
        {
            get
            {
                if (this.bannerRepository == null)
                {
                    this.bannerRepository = new CachedBannerRepository(_context);
                }
                return bannerRepository;
            }
        }

        public IRepository<BannerPlace> BannerPlaces
        {
            get
            {
                if (this.bannerPlaceRepository == null)
                {
                    this.bannerPlaceRepository = new CachedBannerPlaceRepository(_context);
                }
                return bannerPlaceRepository;
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
