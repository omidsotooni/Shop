using Shop.Common.Dto;

namespace Shop.Application.Services.Products.Queries.GetProductForSite
{
    public interface IGetProductForSiteService
    {
        ResultDto<ResultProductForSiteDto> Execute(int Page, long? CategoryId);
    }
}
