using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mishutki.Domain.Abstract;
using Mishutki.Admin.Models;
using Mishutki.Domain.Entities;
using System.Data;
using PagedList;

namespace Mishutki.Admin.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        public int pageSize = 20;
        private IUnitOfWork unitOfWork;

        #region public methods

        public PostController(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public ActionResult Index(int? page)
        {
            var posts = unitOfWork.Posts.GetAll().OrderByDescending(p => p.Created);
            return View(posts.ToPagedList(page ?? 1, pageSize));
        }

        public ActionResult Create()
        {
            AssignedTagData();
            CategoriesDropDownList();
            StatusTypeList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Post post, string[] selectedTags)
        {
            try
            {
                var postToCreate = new Post
                {
                    PostId = post.PostId,
                    Title = post.Title,
                    Content = post.Content,
                    CategoryId = post.CategoryId,
                    StatusTypeId = post.StatusTypeId,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    CreatedByUser = User.Identity.Name
                };
                UpdatePostTags(selectedTags, postToCreate);
                unitOfWork.Posts.Add(postToCreate);
                unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View();
        }

        public ActionResult Edit(int postId)
        {
            Post post = unitOfWork.Posts.GetById(postId);
            if (post == null)
            {
                return HttpNotFound();
            }
            CategoriesDropDownList(post.CategoryId);
            AssignedTagData(post);
            StatusTypeList(post.StatusTypeId);
            return View(post);
        }

        [HttpPost]
        public ActionResult Edit(Post post, string[] selectedTags)
        {
            Post postToUpdate = unitOfWork.Posts.GetById(post.PostId);

            if (TryUpdateModel(postToUpdate, "", null, new string[] { "Tags" }))
            {
                try
                {
                    UpdateModel(postToUpdate, "", null, new string[] { "Tags" });
                    UpdatePostTags(selectedTags, postToUpdate);

                    postToUpdate.Modified = DateTime.Now;
                    postToUpdate.CreatedByUser = User.Identity.Name;

                    unitOfWork.Posts.Update(postToUpdate);
                    unitOfWork.Commit();
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            CategoriesDropDownList(post.CategoryId);
            AssignedTagData(postToUpdate);
            StatusTypeList(post.StatusTypeId);
            return View(postToUpdate);
        }

        [HttpPost]
        public ActionResult Delete(int postId)
        {
            unitOfWork.Posts.Delete(postId);
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
                    unitOfWork.Posts.Delete(item);
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

        #region private methods

        private void CategoriesDropDownList(object selectedCategory = null)
        {
            var categories = unitOfWork.Categories.GetAll();
            ViewBag.CategoryList = new SelectList(categories, "CategoryId", "Title", selectedCategory);
        }

        private void AssignedTagData(Post post = null)
        {
            var allTags = unitOfWork.Tags.GetAll();
            var assignedTags = new List<AssignedTagData>();

            if (post != null)
            {
                var postTags = new HashSet<int>(post.Tags.Select(t => t.TagId));
                foreach (var tag in allTags)
                {
                    assignedTags.Add(new AssignedTagData
                    {
                        TagId = tag.TagId,
                        Title = tag.Title,
                        Assigned = postTags.Contains(tag.TagId)
                    });
                }
                ViewBag.Tags = assignedTags;
            }
            else
            {
                foreach (var tag in allTags)
                {
                    assignedTags.Add(new AssignedTagData
                    {
                        TagId = tag.TagId,
                        Title = tag.Title,
                        Assigned = false
                    });
                }
                ViewBag.Tags = assignedTags;
            }
        }

        private void UpdatePostTags(string[] selectedTags, Post postToUpdate)
        {
            if (selectedTags == null)
            {
                postToUpdate.Tags.Clear();
                return;
            }

            var selectedTagsHS = new HashSet<string>(selectedTags);
            var postTags = new HashSet<int>(postToUpdate.Tags.Select(t => t.TagId));
            foreach (var tag in unitOfWork.Tags.GetAll())
            {
                if (selectedTagsHS.Contains(tag.TagId.ToString()))
                {
                    if (!postTags.Contains(tag.TagId))
                    {
                        postToUpdate.Tags.Add(tag);
                    }
                }
                else
                {
                    if (postTags.Contains(tag.TagId))
                    {
                        postToUpdate.Tags.Remove(tag);
                    }
                }
            }
        }

        private void StatusTypeList(object selectedStatusType = null)
        {
            var statusTypeList = from StatusType s in Enum.GetValues(typeof(StatusType))
                                 select new { Id = (int)s, Name = s.ToString() };
            ViewBag.StatusTypeList = new SelectList(statusTypeList, "Id", "Name", selectedStatusType);
        }

        #endregion

        private enum StatusType { None = 119, Published = 121 }
    }
}