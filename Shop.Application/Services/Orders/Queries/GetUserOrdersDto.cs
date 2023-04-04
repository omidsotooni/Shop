using static Shop.Common.Utility;

namespace Shop.Application.Services.Orders.Queries
{
    public class GetUserOrdersDto
    {
        public long OrderId { get; set; }
        public OrderState OrderState { get; set; }
        public long PaymentId { get; set; }
        public DateTime InsertDate { get; set; }
        public List<OrderDetailsDto> OrderDetails { get; set; }
    }
    public class GetUserOrdersForSiteDto
    {
        public List<GetUserOrdersDto> GetUserOrdersDtos { get; set; }
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
