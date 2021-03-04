using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mishutki.Domain.Abstract;
using Mishutki.Domain.Entities;

namespace Mishutki.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public ActionResult Index(int id)
        {
            var posts = unitOfWork.Posts.Find(p => p.CategoryId == id);
            return View(posts);
        }

        public PartialViewResult GetCategories()
        {
            var categories = unitOfWork.Categories.GetAll();
            return PartialView(categories);
        }
	}
}