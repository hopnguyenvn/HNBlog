
namespace HNBlog.Web.Mvc.Controllers.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class PostViewModel
    {
        public int PostID { get; set; }
        public string Title { get; set; }
        [DisplayFormat(DataFormatString="")]// shorten data
        public string PostContent { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}