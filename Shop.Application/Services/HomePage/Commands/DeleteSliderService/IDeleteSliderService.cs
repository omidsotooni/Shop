using Shop.Common.Dto;

namespace Shop.Application.Services.HomePage.Commands.DeleteSliderService
{
    public interface IDeleteSliderService
    {
        ResultDto DeleteSlider(long SliderId);
    }
}
