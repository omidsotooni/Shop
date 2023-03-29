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

        #endregion
    }
}
