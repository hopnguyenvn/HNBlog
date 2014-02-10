namespace HNBlog.Tests
{
    using System;
    using SharpArch.Testing.NUnit;
    using SharpArch.Testing.NUnit.NHibernate;
    using SharpArch.NHibernate;
    using SharpArch.NHibernate.Contracts.Repositories;
    using NUnit.Framework;
    using global::HNBlog.Web.Mvc.Controllers.Queries.Posts;
    using global:: HNBlog.Domain;
    [TestFixture]
    public class PostsQueryObjectTests : RepositoryTestsBase
    {
        private readonly PostListQuery postsListQuery = new PostListQuery();
        private readonly INHibernateRepository<Post> postRepository = new NHibernateRepository<Post>();
        protected override void SetUp()
        {
            ServiceLocatorInitializer.Init();
            base.SetUp();
        }
        [Test]
        public void CanGetViewModel()
        {
            var postViewModels = this.postsListQuery.GetPagedList(1, 50);
            postViewModels.ShouldNotBeNull();
            
            postViewModels.TotalItems.ShouldEqual(1);
        }

        protected override void LoadTestData()
        {
            this.CreatePersistedPosts();
        }

        private void CreatePersistedPosts()
        {
            //Blog mockBlog = MockRepository.GenerateStub<Blog>();
            //mockBlog.Stub(x => x.Id).Return(1);
            var post = new Post { Title = "MockTitle", PostContent = "MockContent" };
            this.postRepository.SaveOrUpdate(post);
            FlushSessionAndEvict(post);
        }

    }
  
}
