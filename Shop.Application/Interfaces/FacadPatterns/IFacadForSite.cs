using Shop.Application.Services.HomePage.Commands.AddNewSlider;

namespace Shop.Application.Interfaces.FacadPatterns
{
    public interface IFacadForSite
    {
        /// <summary>
        /// Add New Slider 
        /// </summary>
        IAddNewSliderService AddNewSliderService { get; }
    }
}
