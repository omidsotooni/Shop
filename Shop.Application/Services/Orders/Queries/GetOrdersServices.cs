using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Contexts;
using Shop.Common;
using Shop.Common.Dto;
using static Shop.Common.Utility;

namespace Shop.Application.Services.Orders.Queries
{
    public class GetOrdersServices : IGetOrdersServices
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public GetOrdersServices(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public ResultDto<OrdersForAdminDto> GetOrdersForAdmin(OrderState orderState, int Page = 1, int PageSize = 20)
        {
            try
            {
                int rowCount = 0;
                var orders = _context.Orders.Include(o => o.OrderDetails).Where(o => o.OrderState == orderState)
                     .OrderByDescending(o => o.Id).ToPaged(Page, PageSize, out rowCount).ToList()
                     .Select(o => new OrdersDto
                     {
                         InsetTime = o.InsertTime,
                         OrderId = o.Id,
                         OrderState = o.OrderState,
                         ProductCount = o.OrderDetails.Count(),
                         PaymentId = o.PaymentId,
                         UserId = o.UserId,
                     }).ToList();
                return new ResultDto<OrdersForAdminDto>()
                {
                    Data = new OrdersForAdminDto()
                    {
                        Orders = orders,
                        CurrentPage = Page,
                        PageSize = PageSize,
                        RowCount = rowCount,
                    },
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                Utility.ExceptionMessage(ex);
                return new ResultDto<OrdersForAdminDto>()
                {
                    IsSuccess = false,
                };
            }
        }
        public ResultDto<GetUserOrdersForSiteDto> GetOrdersForUser(long UserId, int Page = 1, int PageSize = 20)
        {
            try
            {
                int rowCount = 0;
                var orders = _context.Orders
                    .Include(o => o.OrderDetails)
                    .ThenInclude(o => o.Product)
                    .Where(o => o.UserId == UserId)
                    .OrderByDescending(o => o.Id).ToPaged(Page, PageSize, out rowCount).ToList().Select(o => new GetUserOrdersDto
                    {
                        OrderId = o.Id,
                        OrderState = o.OrderState,
                        PaymentId = o.PaymentId,
                        InsertDate = o.InsertTime,

                        OrderDetails = o.OrderDetails.Select(x => new OrderDetailsDto
                        {
                            Count = x.Count,
                            OrderDetailId = x.Id,
                            Price = x.Price,
                            ProductId = x.ProductId,
                            ProductName = x.Product.Name,
                        }).ToList(),
                    }).ToList();

                return new ResultDto<GetUserOrdersForSiteDto>()
                {
                    Data = new GetUserOrdersForSiteDto()
                    {
                        CurrentPage = Page,
                        PageSize = PageSize,
                        RowCount = rowCount,
                        GetUserOrdersDtos = orders,
                    },                    
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                Utility.ExceptionMessage(ex);
                return new ResultDto<GetUserOrdersForSiteDto>()
                {
                    IsSuccess = false,
                };
            }
        }

        #endregion
    }
}
