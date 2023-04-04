using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Contexts;
using Shop.Common;
using Shop.Common.Dto;
using Shop.Domain.Entities.Orders;
using static Shop.Common.Utility;

namespace Shop.Application.Services.Orders.Commands
{
    public class OrderServices : IOrderServices
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public OrderServices(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public ResultDto AddNewOrder(RequestAddNewOrderSericeDto request)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                var user = _context.Users.Find(request.UserId);
                var payment = _context.Payments.Find(request.PaymentId);
                var cart = _context.Carts.Include(o => o.CartItems).ThenInclude(o => o.Product)
                    .Where(o => o.Id == request.CartId).FirstOrDefault();
                if(user != null && payment != null && cart != null)
                {
                    payment.IsPaid = request.Status;
                    payment.PayDate = DateTime.Now;
                    payment.RefId = request.RefId;
                    payment.Authority = request.Authority;
                    cart.Finished = request.Status;
                    cart.Expired = request.Status;
                    Order order = new Order()
                    {
                        Address = request.Address,
                        OrderState = OrderState.Processing,
                        Payment = payment,
                        User = user,
                        BankingGateway = request.BankingGateway,
                    };
                    _context.Orders.Add(order);

                    List<OrderDetail> orderDetails = new List<OrderDetail>();
                    foreach (var item in cart.CartItems)
                    {
                        OrderDetail orderDetail = new OrderDetail()
                        {
                            Count = item.Count,
                            Order = order,
                            Price = item.Product.Price,
                            Product = item.Product,
                        };
                        orderDetails.Add(orderDetail);
                    }
                    _context.OrderDetails.AddRange(orderDetails);
                    _context.SaveChanges();
                    transaction.Commit();
                    return new ResultDto()
                    {
                        IsSuccess = true,
                        Message = "سفارش با موفقیت ثبت شد.",
                    };
                }
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "ثبت سفارش ناموفق!",
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Utility.ExceptionMessage(ex);
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "سفارش ثبت نشد"
                };
            }
        }

        #endregion
    }
}
