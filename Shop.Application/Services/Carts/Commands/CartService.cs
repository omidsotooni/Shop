using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Contexts;
using Shop.Common;
using Shop.Common.Dto;
using Shop.Domain.Entities.Carts;

namespace Shop.Application.Services.Carts.Commands
{
    public class CartService : ICartService
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public CartService(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public ResultDto Add(long CartItemId)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                var cartItem = _context.CartItems.Find(CartItemId);
                if (cartItem == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                    };
                }
                cartItem.Count++;
                _context.SaveChanges();
                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Utility.ExceptionMessage(ex);
                return new ResultDto()
                {
                    IsSuccess = false,
                };
            }
        }

        public ResultDto AddToCart(long ProductId, Guid BrowserId)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                var product = _context.Products.Find(ProductId);
                if (product == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "محصول مورد نظر پیدا نشد!",
                    };
                }
                var cart = _context.Carts.Where(o => o.BrowserId == BrowserId && o.Expired == false).FirstOrDefault();
                if (cart == null)
                {
                    Cart newCart = new Cart()
                    {
                        Expired = false,
                        BrowserId = BrowserId,
                    };
                    _context.Carts.Add(newCart);
                    _context.SaveChanges();
                    cart = newCart;
                }
                var cartItem = _context.CartItems.Where(o => o.ProductId == ProductId && o.CartId == cart.Id).FirstOrDefault();
                if (cartItem == null)
                {
                    CartItem newCartItem = new CartItem()
                    {
                        Cart = cart,
                        Count = 1,
                        Price = product.Price,
                        Product = product,
                    };
                    _context.CartItems.Add(newCartItem);
                    _context.SaveChanges();
                }
                else
                {
                    cartItem.Count++;
                    _context.SaveChanges();
                }
                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = $"محصول  {product.Name} به سبد خرید شما اضافه شد ",
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Utility.ExceptionMessage(ex);
                return new ResultDto()
                {
                    IsSuccess = false,
                };
            }
        }

        public ResultDto LowOff(long CartItemId)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                var cartItem = _context.CartItems.Find(CartItemId);
                if (cartItem == null || cartItem.Count <= 1)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                    };
                }
                cartItem.Count --;
                _context.SaveChanges();
                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Utility.ExceptionMessage(ex);
                return new ResultDto()
                {
                    IsSuccess = false,
                };
            }
        }

        public ResultDto RemoveFromCart(long ProductId, Guid BrowserId)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                var cartitem = _context.CartItems.Where(o => o.Cart.BrowserId == BrowserId).FirstOrDefault();
                if (cartitem == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "سبد خرید شما خالی است!"
                    };
                }
                cartitem.IsRemoved = true;
                cartitem.RemoveTime = DateTime.Now;
                _context.SaveChanges();
                transaction.Commit();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "محصول از سبد خرید شما حذف شد"
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Utility.ExceptionMessage(ex);
                return new ResultDto()
                {
                    IsSuccess = false,
                };
            }
        }

        public ResultDto SetUserForCart(Cart Cart, long? UserId)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                if (UserId == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                    };
                }
                var user = _context.Users.Find(UserId);
                if (user == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                    };
                }
                Cart.User = user;
                _context.SaveChanges();
                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Utility.ExceptionMessage(ex);
                return new ResultDto()
                {
                    IsSuccess = false,
                };
            }
        }

        public ResultDto<CartDto> GetMyCart(Guid BrowserId, long? UserId)
        {
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
                        CartId = cart.Id,
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
