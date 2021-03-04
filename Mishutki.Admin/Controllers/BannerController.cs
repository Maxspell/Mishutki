using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mishutki.Domain.Abstract;
using Mishutki.Admin.Models;
using Mishutki.Domain.Entities;
using PagedList;
using System.IO;

namespace Mishutki.Admin.Controllers
{
    public class BannerController : Controller
    {
        public int pageSize = 10;
        private IUnitOfWork unitOfWork;

        #region public methods

        public BannerController(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public ActionResult Index(int? page)
        {
            var banners = unitOfWork.Banners.GetAll(orderBy: b => b.OrderByDescending(d => d.Created), includeProperties: "BannerPlace");
            return View(banners.ToPagedList(page ?? 1, pageSize));
        }

        public ViewResult Create()
        {
            ViewBag.RdbText = false;
            ViewBag.RdbUpload = true;
            BannerPlacesDropDownList();
            return View(new Banner());
        }

        [HttpPost]
        public ActionResult Create(Banner banner, HttpPostedFileBase picture, string choices)
        {
            if (ModelState.IsValid)
            {
                var pictureName = banner.ImageUrl;
                if (picture != null && picture.ContentLength > 0 && choices == "rdbUpload")
                {
                    pictureName = Path.GetFileName(picture.FileName);
                    var path = Path.Combine(Server.MapPath("~/content/upload/images"), pictureName);
                    picture.SaveAs(path);
                }

                unitOfWork.Banners.Add(new Banner()
                {
                    Title = banner.Title,
                    ImageUrl = pictureName,
                    Link = banner.Link,
                    Content = banner.Content,
                    Order = banner.Order,
                    BannerPlaceId = banner.BannerPlaceId,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    CreatedByUser = User.Identity.Name
                });
                unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return View(banner);
            }
        }

        public ActionResult Edit(int bannerId)
        {
            Banner banner = unitOfWork.Banners.GetById(bannerId);
            if (banner == null)
            {
                return HttpNotFound();
            }

            if (!String.IsNullOrEmpty(banner.ImageUrl))
            {
                ViewBag.RdbText = true;
                ViewBag.RdbUpload = false;
            }
            else
            {
                ViewBag.RdbText = false;
                ViewBag.RdbUpload = true;
            }

            BannerPlacesDropDownList(banner.BannerPlaceId);
            return View(banner);
        }

        [HttpPost]
        public ActionResult Edit(Banner banner, HttpPostedFileBase picture, string choices)
        {
            if (ModelState.IsValid)
            {
                Banner bannerToUpdate = unitOfWork.Banners.GetById(banner.BannerId);

                var pictureName = banner.ImageUrl;
                if (picture != null && picture.ContentLength > 0 && choices == "rdbUpload")
                {
                    pictureName = Path.GetFileName(picture.FileName);
                    var path = Path.Combine(Server.MapPath("~/content/upload/images"), pictureName);
                    picture.SaveAs(path);
                }

                bannerToUpdate.Title = banner.Title;
                bannerToUpdate.ImageUrl = pictureName;
                bannerToUpdate.Link = banner.Link;
                bannerToUpdate.Content = banner.Content;
                bannerToUpdate.Order = banner.Order;
                bannerToUpdate.BannerPlaceId = banner.BannerPlaceId;
                bannerToUpdate.Modified = DateTime.Now;
                bannerToUpdate.CreatedByUser = User.Identity.Name;

                unitOfWork.Banners.Update(bannerToUpdate);
                unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                BannerPlacesDropDownList(banner.BannerPlaceId);
                return View(banner);
            }
        }

        [HttpPost]
        public ActionResult Delete(int bannerId)
        {
            unitOfWork.Banners.Delete(bannerId);
            unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ManyDelete(int[] deleteItems)
        {
            if (deleteItems == null)
            {
                ModelState.AddModelError("", "None of the reconds has been selected for delete action !");
                return RedirectToAction("Index");
            }

            foreach (var item in deleteItems)
            {
                try
                {
                    unitOfWork.Banners.Delete(item);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed On Id " + item.ToString() + " : " + ex.Message);
                    return RedirectToAction("Index");
                }
            }
            unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        #endregion

        private void BannerPlacesDropDownList(object selectedBannerPlace = null)
        {
            var bannerPlaces = unitOfWork.BannerPlaces.GetAll();
            ViewBag.BannerPlaceList = new SelectList(bannerPlaces, "BannerPlaceId", "Title", selectedBannerPlace);
        }
	}
}