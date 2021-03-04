using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using PagedList;
using Mishutki.Domain.Abstract;
using Mishutki.Domain.Entities;
using Mishutki.WebUI.Models;
using Microsoft.AspNet.Identity;

namespace Mishutki.WebUI.Controllers
{
    public class PostController : Controller
    {
        private IUnitOfWork unitOfWork;

        public PostController(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult PostRating(int id, string vote)
        {
            Post post = unitOfWork.Posts.GetById(id);

            PostRating postRating = new PostRating();
            postRating.PostId = id;
            postRating.UserId = User.Identity.GetUserId();
            postRating.Created = DateTime.Now;

            switch(vote)
            {
                case "like":
                    post.Rating = post.Rating + 1;
                    postRating.Like = true;
                    ViewBag.Like = true;
                    break;
                case "dislike":
                    post.Rating = post.Rating - 1;
                    postRating.Like = false;
                    ViewBag.Like = false;
                    break;
            }

            unitOfWork.Posts.Update(post);
            unitOfWork.PostRatings.Add(postRating);
            unitOfWork.Commit();
            return PartialView(post);
        }

        [Authorize]
        public ActionResult SendJoke()
        {
            CategoriesDropDownList();
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult SendJoke(Post post)
        {
            try
            {
                var postToAdd = new Post
                {
                    Title = post.Title,
                    Content = post.Content,
                    CategoryId = post.CategoryId,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    CreatedByUser = User.Identity.Name
                };
                unitOfWork.Posts.Add(postToAdd);
                unitOfWork.Commit();
                return View("SendJokeConfirmation");
            }
            catch (DataException)
            {
                return View("Error");
            }
        }


        #region private methods

        private void CategoriesDropDownList(object selectedCategory = null)
        {
            var categories = unitOfWork.Categories.GetAll();
            ViewBag.CategoryList = new SelectList(categories, "CategoryId", "Title", selectedCategory);
        }

        #endregion
    }
}