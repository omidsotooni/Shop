using Shop.Domain.Entities.Commons;

namespace Shop.Domain.Entities.Blog
{
    public class BlogCategory : BaseEntity
    {
        public string CategoryText { get; set; }
        public virtual ICollection<BlogEntity> BlogEntities { get; set; }
    }
}
