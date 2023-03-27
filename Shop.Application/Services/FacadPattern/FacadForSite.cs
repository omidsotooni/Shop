using Microsoft.AspNetCore.Hosting;
using Shop.Application.Interfaces.Contexts;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Application.Services.HomePage.Commands.AddNewSlider;
using Shop.Application.Services.HomePage.Commands.DeleteSliderService;
using Shop.Application.Services.HomePage.Commands.EditSliderService;
using Shop.Application.Services.HomePage.Commands.HomePageImagesService;
using Shop.Application.Services.HomePage.Commands.SliderSatusChangeService;
using Shop.Application.Services.HomePage.Queries.GetSliderForAdminService;

namespace Shop.Application.Services.FacadPattern
{
    public class FacadForSite : IFacadForSite
    {
        #region Fields
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        #endregion

        #region Constructor
        public FacadForSite(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        #endregion

        #region Methods
        private IAddNewSliderService _addNewSliderService;
        public IAddNewSliderService AddNewSliderService
        {
            get
            {
                return _addNewSliderService = _addNewSliderService ?? new AddNewSliderService(_context, _environment);
            }
        }
        private IGetSliderForAdminService _getSliderForAdminService;
        public IGetSliderForAdminService GetSliderForAdminService
        {
            get
            {
                return _getSliderForAdminService = _getSliderForAdminService ?? new GetSliderForAdminService(_context);
            }
        }

        private IEditSliderService _editSliderService;
        public IEditSliderService EditSliderService
        {
            get
            {
                return _editSliderService = _editSliderService ?? new EditSliderService(_context, _environment);
            }
        }
        private ISliderSatusChangeService _sliderSatusChangeService;
        public ISliderSatusChangeService SliderSatusChangeService
        {
            get
            {
                return _sliderSatusChangeService = _sliderSatusChangeService ?? new SliderSatusChangeService(_context);
            }
        }

        private IDeleteSliderService _deleteSliderService;
        public IDeleteSliderService DeleteSliderService
        {
            get
            {
                return _deleteSliderService = _deleteSliderService ?? new DeleteSliderService(_context);
            }
        }

        private IHomePageImagesService _homePageImagesService;
        public IHomePageImagesService HomePageImagesService
        {
            get
            {
                return _homePageImagesService = _homePageImagesService ?? new HomePageImagesService(_context, _environment);
            }
        }

        #endregion
    }
}
