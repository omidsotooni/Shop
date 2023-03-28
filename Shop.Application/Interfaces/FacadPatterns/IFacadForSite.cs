using Shop.Application.Services.HomePage.Commands.AddNewSlider;
using Shop.Application.Services.HomePage.Commands.DeleteSliderService;
using Shop.Application.Services.HomePage.Commands.EditSliderService;
using Shop.Application.Services.HomePage.Commands.HomePageImagesService;
using Shop.Application.Services.HomePage.Commands.SliderSatusChangeService;
using Shop.Application.Services.HomePage.Queries.GetHomePageImagesService;
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
        /// <summary>
        /// Edit Slider
        /// </summary>
        IEditSliderService EditSliderService { get; }
        /// <summary>
        /// Change Satus of Slider
        /// </summary>
        ISliderSatusChangeService SliderSatusChangeService { get; }
        /// <summary>
        /// Delete Slider
        /// </summary>
        IDeleteSliderService DeleteSliderService { get; }
        /// <summary>
        /// Home Page Images Service includes Add, Edit, Delete
        /// </summary>
        IHomePageImagesService HomePageImagesService { get; }
        /// <summary>
        /// Get Home Page Images for admin
        /// </summary>
        IGetHomePageImagesService GetHomePageImagesService { get; }
    }
}
