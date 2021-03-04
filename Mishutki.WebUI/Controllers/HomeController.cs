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
    public class HomeController : Controller
    {
        public int pageSize = 10;
        private IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Posts(string category, string sort, int? page)
        {
            IEnumerable<Post> posts;
            if (string.IsNullOrEmpty(category))
            {
                posts = unitOfWork.Posts.GetAll(
                    filter: p => p.StatusTypeId == 121,
                    orderBy: p => p.OrderByDescending(d => d.Created),
                    includeProperties: "Category,Tags,PostRatings");
            }
            else
            { 
                posts = unitOfWork.Posts.GetAll(
                    filter: p => p.Category.Alias.Contains(category) && p.StatusTypeId == 121,
                    orderBy: p => p.OrderByDescending(d => d.Created),
                    includeProperties: "Category,Tags,PostRatings");
            }

            switch(sort)
            {
                case "date_asc":
                    posts = posts.OrderBy(p => p.Created);
                    ViewBag.SortParamAsc = "date_asc";
                    ViewBag.SortParamDesc = "date";
                    break;
                case "date":
                    posts = posts.OrderByDescending(p => p.Created);
                    ViewBag.SortParamAsc = "date_asc";
                    ViewBag.SortParamDesc = "date";
                    break;
                case "rating_asc":
                    posts = posts.OrderBy(p => p.Rating);
                    ViewBag.SortParamAsc = "rating_asc";
                    ViewBag.SortParamDesc = "rating";
                    break;
                case "rating":
                    posts = posts.OrderByDescending(p => p.Rating);
                    ViewBag.SortParamAsc = "rating_asc";
                    ViewBag.SortParamDesc = "rating";
                    break;
                default:
                    ViewBag.SortParamAsc = "date_asc";
                    ViewBag.SortParamDesc = "date";
                    break;
            }

            return PartialView("_PostsPartial", posts.ToPagedList(page ?? 1, pageSize));
        }


        public PartialViewResult GetTags()
        {
            var tags = unitOfWork.Tags.GetAll();
            return PartialView(tags);
        }

        public PartialViewResult GetBanners(int bannerPlaceId)
        {
            var banners = unitOfWork.Banners.GetAll(
                filter: b => b.BannerPlaceId == bannerPlaceId, 
                orderBy: b => b.OrderBy(d => d.Order));
            return PartialView(banners);
        }

        public ActionResult Contacts()
        {
            return View("Contacts");
        }

        public ActionResult About()
        {
            return View("About");
        }
    }
}