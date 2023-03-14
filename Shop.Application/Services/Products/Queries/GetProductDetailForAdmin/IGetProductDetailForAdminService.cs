using Shop.Common.Dto;

namespace Shop.Application.Services.Products.Queries.GetProductDetailForAdmin
{
    public interface IGetProductDetailForAdminService
    {
        ResultDto<ProductDetailForAdmindto> Execute(long Id);
    }
}
