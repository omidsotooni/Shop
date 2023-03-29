using Shop.Domain.Entities.Commons;
using Shop.Domain.Entities.Users;

namespace Shop.Domain.Entities.Carts
{
    public class Cart : BaseEntity
    {
        public virtual User User { get; set; }
        public long? UserId { get; set; }
        public Guid BrowserId { get; set; }
        public bool Expired { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
