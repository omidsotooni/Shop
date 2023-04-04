using static Shop.Common.Utility;

namespace Shop.Application.Services.Orders.Queries
{
    public class OrdersDto
    {
        public long OrderId { get; set; }
        public DateTime InsetTime { get; set; }
        public long PaymentId { get; set; }
        public long UserId { get; set; }
        public OrderState OrderState { get; set; }
        public int ProductCount { get; set; }       
    }
    public class OrdersForAdminDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<OrdersDto> Orders { get; set; }
    }
}
