
namespace HNBlog.Tasks.Commands
{
    using SharpArch.Domain.Commands;
    using System.ComponentModel.DataAnnotations;
    using System;
    public class ChangePostDetailCommand : CommandBase
    {
        public ChangePostDetailCommand(
                                            int id,
                                            string title,
                                            string postContent)
        {
            this.PostID = id;
            this.Title = title;
            this.PostContent = postContent;
            //this.CreatedDate = id == default(int)? DateTime.Now:;
        }

        [Required]
        public int PostID { get; set; }

        [Required]
        public string Title { get; set; }

        public string PostContent { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

    }
}