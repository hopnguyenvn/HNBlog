
namespace HNBlog.Web.Mvc.Controllers.Queries.Posts
{
    using SharpArch.NHibernate;
    using MvcContrib.Pagination;
    using HNBlog.Web.Mvc.Controllers.ViewModels;
    using HNBlog.Domain;
    using NHibernate.Transform;
using System.Collections.Generic;
    using System.Text.RegularExpressions;
    public class PostListQuery : NHibernateQuery, IPostListQuery
    {


        public IPagination<PostViewModel> GetPagedList(int page, int size)
        {
            var query = Session.QueryOver<Post>().OrderBy(x => x.CreatedDate).Desc;

            var count = query.ToRowCountQuery();
            var totalCount = count.FutureValue<int>();

            var firstResult = (page - 1) * size;

            PostViewModel viewModel = null;
            //Blog blog = null;

            var viewModels =
               query.SelectList(list => list
                                         .Select(x => x.Id).WithAlias(() => viewModel.PostID)
                                         .Select(x => x.PostContent).WithAlias(() => viewModel.PostContent)// summary post content here
                                         .Select(x => x.Title).WithAlias(() => viewModel.Title)
                                         // Flattening the object graph
                                         //.Select(x => blog.Name).WithAlias(() => viewModel.Name))
                                         .Select(x => x.CreatedDate).WithAlias(() => viewModel.CreatedDate))
                .TransformUsing(Transformers.AliasToBean(typeof(PostViewModel)))
                .Skip(firstResult)
                .Take(size)
                .Future<PostViewModel>();

            return new CustomPagination<PostViewModel>(viewModels, page, size, totalCount.Value);
        }
        public IList<PostViewModel> GetPosts()
        {
            PostViewModel viewModel = null;
            var postViewModels = Session.QueryOver<Post>().OrderBy(x => x.CreatedDate).Desc
                .SelectList(list => list
                                         .Select(x => x.Id).WithAlias(() => viewModel.PostID)
                                         .Select(x => x.PostContent).WithAlias(() => viewModel.PostContent)// summary post content here
                                         .Select(x => x.Title).WithAlias(() => viewModel.Title)
                                         .Select(x => x.CreatedDate).WithAlias(() => viewModel.CreatedDate))
                .TransformUsing(Transformers.AliasToBean(typeof(PostViewModel))).List<PostViewModel>();
            // Manual transform view model one more time to shorten the post content
            
            for(int i = 0; i<postViewModels.Count; i++)
            {
                postViewModels[i].PostContent = Common.ShortenString(postViewModels[i].PostContent);
            }
            return postViewModels;
        }
        

        

    }
}