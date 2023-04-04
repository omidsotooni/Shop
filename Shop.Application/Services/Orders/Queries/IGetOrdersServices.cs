using Shop.Common.Dto;
using static Shop.Common.Utility;

namespace Shop.Application.Services.Orders.Queries
{
    public interface IGetOrdersServices
    {
        ResultDto<OrdersForAdminDto> GetOrdersForAdmin(OrderState orderState, int Page = 1, int PageSize = 20);
        ResultDto<GetUserOrdersForSiteDto> GetOrdersForUser(long UserId, int Page = 1, int PageSize = 20);
    }
}
