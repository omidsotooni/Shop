using Shop.Common.Dto;
using Shop.Domain.Entities.Carts;

namespace Shop.Application.Services.Carts.Commands
{
    public interface ICartService
    {
        ResultDto AddToCart(long ProductId, Guid BrowserId);
        ResultDto RemoveFromCart(long ProductId, Guid BrowserId);
        ResultDto SetUserForCart(Cart Cart, long? UserId);
        ResultDto Add(long CartItemId);
        ResultDto LowOff(long CartItemId);
    }
}
