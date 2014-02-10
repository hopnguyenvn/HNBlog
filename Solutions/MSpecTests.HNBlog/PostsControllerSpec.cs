using System;
using System.Web.Mvc;
//using  Machine.Specifications;
using HNBlog.Web.Mvc.Controllers.Queries.Posts;
using HNBlog.Web.Mvc.Controllers;
using Rhino.Mocks;
using NUnit.Framework;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate.Contracts.Repositories;
using HNBlog.Domain;
using System.Collections.Generic;
using MvcContrib.TestHelper;
using HNBlog.Web.Mvc.Controllers.ViewModels;
using HNBlog.Tasks;
using SharpArch.Testing.NUnit.NHibernate;

namespace MSpecTests.HNBlog
{
    
    [TestFixture]
    public class PostsControllerSpec
    {
        private IPostListQuery postsListQuery;
        private IBlogTasks blogRepository;
        private BlogController controller;
     
        [SetUp]
        protected void Setup()
        {
            postsListQuery = new PostListQuery();
            blogRepository = CreateMockIBlogTask();
            controller = new BlogController(postsListQuery,
                               blogRepository);
            //ServiceLocatorInitializer.Init();
            //base.SetUp();
        }
        
        //[Test]
        //public void CanListPostsWithCreatedDateOrderDescending()
        //{

        //    ViewResult result =
        //        controller.Post()
        //            .AssertViewRendered()
        //            .ForView("Post");

        //    Assert.That(result.ViewData, Is.Not.Null);
        //    var models =  result.ViewData.Model as List<PostViewModel>;
        //    Assert.That(models, Is.Not.Null);

        //    Assert.That((models).Count, Is.EqualTo(1));
        //    //var test = result.ViewData.Model as IEnumerable<PostViewModel>;
        //    //Assert.IsTrue(test.SequenceEqual(
        //    //           test.OrderDescendingBy(e => e.CreatedDate)));
        //}
        [Test]
        public void CanListPostDetailsById()
        {
            ViewResult result =
                controller.Details(1)
                    .AssertViewRendered()
                    .ForView("Details");

            Assert.That(result.ViewData, Is.Not.Null);
            Assert.That(result.ViewData.Model as
              Post, Is.Not.Null);
            Assert.IsTrue((result.ViewData.Model as
              Post).Id == 1);
        }
                
        /// </summary>
        public IBlogTasks CreateMockIBlogTask()
        {
            MockRepository mocks = new MockRepository();

            IBlogTasks mockedRepository =
                mocks.StrictMock<IBlogTasks>();
            Blog mockBlog = MockRepository.GenerateStub<Blog>();
            mockBlog.Stub(x => x.Id).Return(1);
            Expect.Call(mockedRepository.Posts())
                .IgnoreArguments()
                .Return(CreatePosts(mockBlog));
            mocks.Replay(mockedRepository);

            return mockedRepository;
        }
        private List<Post> CreatePosts(Blog blog)
        {
            List<Post> posts =
                new List<Post>();

            // set default blog
            var post = MockRepository.GenerateStub<Post>();
            //post.Title= "Title 1";
            //post.PostContent = "Content 1";
            //post.Blog = blog;
            //post.CreatedDate = DateTime.Now;
            post.Stub(x => x.Id).Return(1);
            posts.Add(post);
            //post = new Post() { Title = "Title 2", PostContent = "Content 2", Blog = blog, CreatedDate = DateTime.Now };
            //post.Stub(x => x.Id).Return(2);
            //post = new Post() { Title = "Title 3", PostContent = "Content 3", Blog = blog, CreatedDate = DateTime.Now };
            //post.Stub(x => x.Id).Return(3);
            //post = new Post() { Title = "Title 4", PostContent = "Content 4", Blog = blog, CreatedDate = DateTime.Now };
            //post.Stub(x => x.Id).Return(4);
            //posts.Add(new Post() { Title = "Title 2", PostContent = "Content 2", Blog = blog, CreatedDate = DateTime.Now });
            //posts.Add(new Post() { Title = "Title 3", PostContent = "Content 3", Blog = blog, CreatedDate = DateTime.Now });
            //posts.Add(new Post() { Title = "Title 4", PostContent = "Content 4", Blog = blog, CreatedDate = DateTime.Now });

            return posts;
        }

    }


}
