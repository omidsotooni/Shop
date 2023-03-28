using Shop.Common.Dto;

namespace Shop.Application.Services.HomePage.Queries.GetHomePageImagesService
{
    public interface IGetHomePageImagesService
    {
        ResultDto<GetHomePageImaesAdminDto> GetHomePageImagesForAdmin(int Page = 1, int PageSize = 10);
    }
}
