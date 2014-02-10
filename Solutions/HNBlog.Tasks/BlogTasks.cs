using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HNBlog.Domain;
using SharpArch.Domain.PersistenceSupport;

namespace HNBlog.Tasks
{
    public class BlogTasks: IBlogTasks
    {
        private readonly IRepository<Blog> blogRepository;
        private readonly IRepository<Post> postRepository;

        public BlogTasks(IRepository<Blog> blogRepository, IRepository<Post> postRepository)
        {
            
            this.blogRepository = blogRepository;
            this.postRepository = postRepository;
        }
        public Blog Blog(int id)
        {
            return blogRepository.Get(id);
        }
        public Post Post(int id)
        {
            return postRepository.Get(id);
        }
        public void DeletePost(int id)
        {
            // check if the post exists
            var post = postRepository.Get(id);
            if (post != null)
            {
                postRepository.Delete(post);
            }
        }
        public int AddPost(Post post)
        {
            if (post.Id == default(int))
                post.CreatedDate = DateTime.Now;// if post is a new one, set created date is current date
            postRepository.SaveOrUpdate(post);
            return post.Id;
        }
        public IList<Post> Posts()
        {
            return postRepository.GetAll();
        }
    }
}
