using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Contexts;
using Shop.Common;
using Shop.Common.Dto;

namespace Shop.Application.Services.Carts.Queries
{
    public class GetCartService : IGetCartService
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public GetCartService(IDataBaseContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods
        public ResultDto<CartDto> GetMyCart(Guid BrowserId, long? UserId)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                var cart = _context.Carts.Include(o => o.CartItems)
                    .ThenInclude(o => o.Product).ThenInclude(o => o.ProductImages)
                    .Where(o => o.BrowserId == BrowserId && o.Expired == false)
                    .OrderByDescending(o => o.Id).FirstOrDefault();
                if (cart == null)
                {
                    return new ResultDto<CartDto>()
                    {
                        IsSuccess = false,
                    };
                }
                if (UserId != null)
                {
                    var user = _context.Users.Find(UserId);
                    if (user != null)
                    {
                        cart.User = user;
                        _context.SaveChanges();
                    }
                }
                return new ResultDto<CartDto>()
                {
                    Data = new CartDto()
                    {
                        ProductCount = cart.CartItems.Count(),
                        SumAmount = cart.CartItems.Sum(o => o.Price * o.Count),
                        CartItems = cart.CartItems.Select(o => new CartItemDto
                        {
                            Count = o.Count,
                            Price = o.Price,
                            Product = o.Product.Name,
                            Id = o.Id,
                            ProductLink = $"~/Products/Detail/{o.Product.Id}",
                            Images = o.Product?.ProductImages?.FirstOrDefault()?.Src ?? "~/images/ProductHasNoImage.jpg",
                        }).ToList(),
                    },
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Utility.ExceptionMessage(ex);
                return new ResultDto<CartDto>()
                {
                    IsSuccess = false,
                };
            }
        }

        #endregion
    }
}
