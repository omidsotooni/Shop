using Microsoft.AspNetCore.Http;
using Shop.Common.Dto;

namespace Shop.Application.Services.HomePage.Commands.AddNewSlider
{
    public interface IAddNewSliderService
    {
        ResultDto Execute(RequestAddNewSliderDto requestAdd, IFormFile file);
    }
}
