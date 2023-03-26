using Microsoft.AspNetCore.Http;
using Shop.Common.Dto;

namespace Shop.Application.Services.HomePage.Commands.EditSliderService
{
    public interface IEditSliderService
    {
        ResultDto<EditSliderDto> GetSlider(long  sliderId);
        ResultDto<EditSliderDto> EditSlider(EditSliderDto Slider, IFormFile file);        
    }

}
