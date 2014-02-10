using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharpArch.NHibernate.Contracts.Repositories;
using HNBlog.Domain;
using HNBlog.Web.Mvc.Controllers.Queries.Posts;
using HNBlog.Web.Mvc.Controllers.ViewModels;
using SharpArch.NHibernate.Web.Mvc;
using HNBlog.Tasks;

namespace HNBlog.Web.Mvc.Controllers
{
    public class BlogController : Controller
    {
        //
        private IPostListQuery postListQuery;
        private IBlogTasks blogRepository;
        public BlogController(IPostListQuery postListQuery, IBlogTasks blogRepository)
        {
            this.blogRepository = blogRepository;
            this.postListQuery = postListQuery;
        }
        public ActionResult Blog()
        {
            var blog = blogRepository.Blog(1);// get default blog setting;
            ViewData["Name"] = blog.Name;
            ViewData["TagLine"] = blog.TagLine;
            return PartialView();
        }
        public ActionResult Post()
        {
            IList<PostViewModel> models = postListQuery.GetPosts();
            return View(models);
        }
     
        //[ValidateAntiForgeryToken]
        public ActionResult Details(int postID)
        {
            var post = this.blogRepository.Post(postID);
            return View(post);
        }
        [Transaction]
        [HttpPost]
        public JsonResult CreatePost(string title, string content)
        {
            Post post = new Post();
            //post.BlogId = 1;// set default, will be changed in the future when authentication + authorization applied
            post.Blog = blogRepository.Blog(1);
            post.Title = title;
            post.PostContent = content;
            int Id = blogRepository.AddPost(post);
            // shorten the PostContent before sending result back to client
            post.PostContent = Common.ShortenString(post.PostContent);
            return Json(post);
        }
        [Transaction]
        [HttpPost]
        //[ValidateAntiForgeryToken(Salt="Use some salt here")]
        public JsonResult DeletePost(int postID)
        {
             blogRepository.DeletePost(postID);
             return Json(new { success = true, message = "Post deleted successfully" });
        }
    }
}
