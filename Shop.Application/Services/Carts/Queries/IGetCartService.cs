using Shop.Common.Dto;
using Shop.Domain.Entities.Carts;

namespace Shop.Application.Services.Carts.Queries
{
    public interface IGetCartService
    {
        ResultDto<Cart> GetUserCart(long? UserId, long CartId);
    }
}
