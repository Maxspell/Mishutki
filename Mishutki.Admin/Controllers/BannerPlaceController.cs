using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mishutki.Domain.Abstract;
using Mishutki.Admin.Models;
using Mishutki.Domain.Entities;
using PagedList;

namespace Mishutki.Admin.Controllers
{
    public class BannerPlaceController : Controller
    {
        public int pageSize = 10;
        private IUnitOfWork unitOfWork;

        #region public methods

        public BannerPlaceController(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public ActionResult Index(int? page)
        {
            var bannerPlaces = unitOfWork.BannerPlaces.GetAll(orderBy: b => b.OrderByDescending(d => d.Created), includeProperties: "Banners");
            return View(bannerPlaces.ToPagedList(page ?? 1, pageSize));
        }

        public ViewResult Create()
        {
            return View(new BannerPlace());
        }

        [HttpPost]
        public ActionResult Create(BannerPlace bannerPlace)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.BannerPlaces.Add(new BannerPlace()
                {
                    Title = bannerPlace.Title,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    CreatedByUser = User.Identity.Name
                });
                unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return View(bannerPlace);
            }
        }

        public ViewResult Edit(int bannerPlaceId)
        {
            return View(unitOfWork.BannerPlaces.GetById(bannerPlaceId));
        }

        [HttpPost]
        public ActionResult Edit(BannerPlace bannerPlace)
        {
            if (ModelState.IsValid)
            {
                BannerPlace bannerPlaceToUpdate = unitOfWork.BannerPlaces.GetById(bannerPlace.BannerPlaceId);

                bannerPlaceToUpdate.Title = bannerPlace.Title;
                bannerPlaceToUpdate.Modified = DateTime.Now;
                bannerPlaceToUpdate.CreatedByUser = User.Identity.Name;

                unitOfWork.BannerPlaces.Update(bannerPlaceToUpdate);
                unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return View(bannerPlace);
            }
        }

        [HttpPost]
        public ActionResult Delete(int bannerPlaceId)
        {
            unitOfWork.BannerPlaces.Delete(bannerPlaceId);
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
                    unitOfWork.BannerPlaces.Delete(item);
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
	}
}