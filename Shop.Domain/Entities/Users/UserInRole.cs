using Shop.Domain.Entities.Commons;

namespace Shop.Domain.Entities.Users
{
    public class UserInRole : BaseEntity
    {
        public long UserInRoleId { get; set; }
        public virtual User User { get; set; }
        public long UserId { get; set; }
        public virtual Role Role { get; set; }
        public long RoleId { get; set; }
    }
}
