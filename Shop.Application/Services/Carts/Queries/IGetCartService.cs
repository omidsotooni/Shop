using Shop.Common.Dto;

namespace Shop.Application.Services.Carts.Queries
{
    public interface IGetCartService
    {
        ResultDto<CartDto> GetMyCart(Guid BrowserId, long? UserId);
    }
}
