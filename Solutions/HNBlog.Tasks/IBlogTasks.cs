
using HNBlog.Domain;
using System.Collections.Generic;
namespace HNBlog.Tasks
{
    public interface IBlogTasks
    {
        Blog Blog(int id);
        int AddPost(Post post);
        void DeletePost(int id);
        Post Post(int id);
        IList<Post> Posts();
    }
}
