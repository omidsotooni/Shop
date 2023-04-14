using Shop.Domain.Entities.Blog;
using Shop.Domain.Entities.Commons;
using Shop.Domain.Entities.Orders;

namespace Shop.Domain.Entities.Users
{
    public class User: BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string UserUrl { get; set; } = "https://omidsotooni.github.io/";
        public string Description { get; set; }
        public ICollection<UserInRole> UserInRoles { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public ICollection<BlogEntity> BlogEntities { get; set; }
    }
}
