using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Mishutki.Domain.Abstract;
using Mishutki.Admin.Models;


namespace Mishutki.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        private IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            var posts = unitOfWork.Posts.GetAll();
            var catgories = unitOfWork.Categories.GetAll();
            var tags = unitOfWork.Tags.GetAll();
            var users = UserManager.Users;

            HomeViewModel homeViewModel = new HomeViewModel
            {
                PostCount = posts.Count(),
                CategoryCount = catgories.Count(),
                TagCount = tags.Count(),
                UserCount = users.Count(),
                Posts = posts.OrderByDescending(p => p.Created).Take(5).ToList(),
                Categories = catgories.OrderByDescending(c => c.Created).Take(5),
                Tags = tags.OrderByDescending(t => t.Created).Take(5),
                Users = users.Take(10).ToList()
            };

            return View(homeViewModel);
        }
	}
}