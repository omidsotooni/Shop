using Shop.Domain.Entities.Commons;

namespace Shop.Domain.Entities.Products
{
    public class Category: BaseEntity
    {
        public string Name { get; set; }
        public virtual Category ParentCategory { get; set; }
        public long? ParentCategoryId { get; set; }
        //برای نمایش زیر دسته های هر گروه
        public virtual ICollection<Category> SubCategories { get; set; }
    }
}
