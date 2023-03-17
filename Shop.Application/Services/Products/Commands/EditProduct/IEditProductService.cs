using Shop.Common.Dto;
using Shop.Domain.Entities.Products;

namespace Shop.Application.Services.Products.Commands.EditProduct
{
    public interface IEditProductService
    {
        ResultDto<EditProduct> GetProductById(long ProductId);
        ResultDto<EditProduct> Execute(EditProduct Product);
    }
}
