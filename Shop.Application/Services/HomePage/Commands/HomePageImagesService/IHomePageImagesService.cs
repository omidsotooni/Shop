using Shop.Common.Dto;

namespace Shop.Application.Services.HomePage.Commands.HomePageImagesService
{
    public interface IHomePageImagesService
    {
        ResultDto AddHomePageImages(RequestHomePageImagesDto request);
        ResultDto<RequestEditHomePageImagesDto> GetHomePageImageForEdit(long HomePageImageId);
        ResultDto<RequestEditHomePageImagesDto> EditHomePageImage(RequestEditHomePageImagesDto HomePageImageForEdit);
    }
}
