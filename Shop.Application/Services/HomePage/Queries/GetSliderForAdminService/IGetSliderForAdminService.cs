using Shop.Common.Dto;

namespace Shop.Application.Services.HomePage.Queries.GetSliderForAdminService
{
    public interface IGetSliderForAdminService
    {
        ResultDto<SliderForAdminDto> GetSlidersForAdmin(int Page = 1, int PageSize = 20);
    }
}
