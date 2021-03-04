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
    public class CategoryController : Controller
    {
        public int pageSize = 10;
        private IUnitOfWork unitOfWork;

        #region public methods

        public CategoryController(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public ActionResult Index(int? page)
        {
            var categories = unitOfWork.Categories.GetAll().OrderByDescending(p => p.Created);
            return View(categories.ToPagedList(page ?? 1, pageSize));
        }

        public ViewResult Create()
        {
            return View(new Category());
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Categories.Add(new Category()
                {
                    CategoryId = category.CategoryId,
                    Title = category.Title,
                    Alias = category.Alias,
                    Order = category.Order,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    CreatedByUser = User.Identity.Name
                });
                unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        public ViewResult Edit(int categoryId)
        {
            return View(unitOfWork.Categories.GetById(categoryId));
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                Category categoryToUpdate = unitOfWork.Categories.GetById(category.CategoryId);

                categoryToUpdate.Title = category.Title;
                categoryToUpdate.Alias = category.Alias;
                categoryToUpdate.Order = category.Order;
                categoryToUpdate.Modified = DateTime.Now;
                categoryToUpdate.CreatedByUser = User.Identity.Name;

                unitOfWork.Categories.Update(categoryToUpdate);
                unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Delete(int categoryId)
        {
            unitOfWork.Categories.Delete(categoryId);
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
                    unitOfWork.Categories.Delete(item);
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