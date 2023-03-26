using Shop.Common.Dto;

namespace Shop.Application.Services.HomePage.Commands.SliderSatusChangeService
{
    public interface ISliderSatusChangeService
    {
        ResultDto ChangeStatusSlider(long SliderId);
    }
}
