using Shop.Common.Dto;

namespace Shop.Application.Services.Products.Commands.RemoveProduct
{
    public interface IRemoveProductService
    {
        ResultDto Execute(long ProductId);
    }
}
