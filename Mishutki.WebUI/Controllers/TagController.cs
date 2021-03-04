using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Mishutki.Domain.Abstract;
using Mishutki.Domain.Entities;
using Mishutki.WebUI.Models;

namespace Mishutki.WebUI.Controllers
{
    public class TagController : Controller
    {
        public int pageSize = 10;
        private IUnitOfWork unitOfWork;

        public TagController(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Posts(int? page, string tagname)
        {
            Tag tag = unitOfWork.Tags.Find(t => t.Title == tagname).SingleOrDefault();
            List<Post> posts = new List<Post>();
            foreach(var post in tag.Posts.OrderByDescending(p => p.Created).ToList())
            {
                posts.Add(post);
            }
            ViewBag.Tagname = tagname;
            return PartialView("_PostsPartial", posts.ToPagedList(page ?? 1, pageSize));
        }
	}
}