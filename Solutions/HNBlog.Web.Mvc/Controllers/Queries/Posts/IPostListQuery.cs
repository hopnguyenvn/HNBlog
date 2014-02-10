
namespace HNBlog.Web.Mvc.Controllers.Queries.Posts
{
    using MvcContrib.Pagination;
    using HNBlog.Web.Mvc.Controllers.ViewModels;
    using System.Collections.Generic;

    public interface IPostListQuery
    {
        IPagination<PostViewModel> GetPagedList(int page, int size);
        IList<PostViewModel> GetPosts();
    }
}
