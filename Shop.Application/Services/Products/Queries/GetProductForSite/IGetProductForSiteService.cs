using Shop.Common.Dto;

namespace Shop.Application.Services.Products.Queries.GetProductForSite
{
    public interface IGetProductForSiteService
    {
        ResultDto<ResultProductForSiteDto> Execute(string SearchKey, int Page, long? CategoryId);
    }
}
