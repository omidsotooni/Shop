using Shop.Common.Dto;

namespace Shop.Application.Services.HomePage.Queries.GetHomePageImageAndSlidersForSite
{
    public interface IGetHomePageImageAndSlidersForSite
    {
        ResultDto<List<HomePageImagesDto>> GetHomePageImages();
        ResultDto<List<SlidersDto>> GetHomePageSliders();
    }
}
