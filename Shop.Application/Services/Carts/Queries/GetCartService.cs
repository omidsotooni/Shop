using Shop.Common.Dto;
using Shop.Common;
using Shop.Domain.Entities.Carts;
using Shop.Application.Interfaces.Contexts;

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
        public ResultDto<Cart> GetUserCart(long? UserId, long CartId)
        {
            try
            {
                if (UserId != null)
                {
                    var cart = _context.Carts.Where(o => o.UserId == UserId && o.Id == CartId && !o.IsRemoved && !o.Expired).FirstOrDefault();
                    if (cart != null)
                    {
                        return new ResultDto<Cart>()
                        {
                            Data = cart,
                            IsSuccess = true,
                        };
                    }
                }
                return new ResultDto<Cart>() 
                { 
                    IsSuccess = false, 
                };
            }
            catch (Exception ex)
            {
                Utility.ExceptionMessage(ex);
                return new ResultDto<Cart>() 
                { 
                    IsSuccess = false,
                };
            }
        }

        #endregion
    }
}
