namespace HNBlog.Domain
{
    using SharpArch.Domain.DomainModel;
    using System.Collections.Generic;

    public class Blog : Entity
    {        
        public virtual string Name { get; set; }
        public virtual string TagLine { get; set; }
        public virtual string BackgroundImgPath { get; set; }
    }
}