using Shop.Domain.Entities.Commons;
using Shop.Domain.Entities.Finances;
using Shop.Domain.Entities.Users;
using static Shop.Common.Utility;

namespace Shop.Domain.Entities.Orders
{
    public class Order : BaseEntity
    {
        public virtual User User { get; set; }
        public long UserId { get; set; }
        public virtual Payment Payment { get; set; }
        public long PaymentId { get; set; }
        public OrderState OrderState { get; set; }
        public Banking BankingGateway { get; set; }
        public string Address { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }

}
