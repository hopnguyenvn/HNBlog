
namespace HNBlog.Domain
{
    using SharpArch.Domain.DomainModel;
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Post:Entity
    {
        [Required(ErrorMessage="Required")]
        public virtual string Title { get; set; }
        public virtual string PostContent { get; set; }
        [Required]
        public virtual DateTime CreatedDate { get; set; }
        public virtual Blog Blog { get; set; }
    }
}
