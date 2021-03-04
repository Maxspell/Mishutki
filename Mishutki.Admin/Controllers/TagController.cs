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
    public class TagController : Controller
    {
        public int pageSize = 10;
        private IUnitOfWork unitOfWork;

        #region public methods

        public TagController(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public ActionResult Index(int? page)
        {
            var tags = unitOfWork.Tags.GetAll().OrderByDescending(p => p.Created);
            return View(tags.ToPagedList(page ?? 1, pageSize));
        }

        public ViewResult Create()
        {
            return View(new Tag());
        }

        [HttpPost]
        public ActionResult Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Tags.Add(new Tag()
                {
                    TagId = tag.TagId,
                    Title = tag.Title,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    CreatedByUser = User.Identity.Name
                });
                unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return View(tag);
            }
        }

        public ViewResult Edit(int tagId)
        {
            return View(unitOfWork.Tags.GetById(tagId));
        }

        [HttpPost]
        public ActionResult Edit(Tag tag)
        {
            if (ModelState.IsValid)
            {
                Tag tagToUpdate = unitOfWork.Tags.GetById(tag.TagId);

                tagToUpdate.Title = tag.Title;
                tagToUpdate.Modified = DateTime.Now;
                tagToUpdate.CreatedByUser = User.Identity.Name;

                unitOfWork.Tags.Update(tagToUpdate);
                unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return View(tag);
            }
        }

        [HttpPost]
        public ActionResult Delete(int tagId)
        {
            unitOfWork.Tags.Delete(tagId);
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
                    unitOfWork.Tags.Delete(item);
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