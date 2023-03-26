using Shop.Application.Services.HomePage.Commands.AddNewSlider;
using Shop.Application.Services.HomePage.Queries.GetSliderForAdminService;

namespace Shop.Application.Interfaces.FacadPatterns
{
    public interface IFacadForSite
    {
        /// <summary>
        /// Add New Slider 
        /// </summary>
        IAddNewSliderService AddNewSliderService { get; }
        /// <summary>
        /// Get Sliders For Admin
        /// </summary>
        IGetSliderForAdminService GetSliderForAdminService { get; }
    }
}
