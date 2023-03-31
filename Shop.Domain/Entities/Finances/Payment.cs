using Shop.Domain.Entities.Commons;
using Shop.Domain.Entities.Users;

namespace Shop.Domain.Entities.Finances
{
    public class Payment : BaseEntity
    {
        public Guid PaymentGuid { get; set; }
        public virtual User User { get; set; }
        public long UserId { get; set; }
        public int Amount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PayDate { get; set; }
        public string Authority { get; set; }
        public long RefId { get; set; } = 0;
    }
}
