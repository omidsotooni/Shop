using Shop.Domain.Entities.Commons;

namespace Shop.Domain.Entities.Blog
{
    public class FAQBlog : BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public long BlogId { get; set; }
        public virtual BlogEntity BlogEntity { get; set; }
    }
}
