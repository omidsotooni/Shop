using Microsoft.AspNetCore.Hosting;
using Shop.Application.Interfaces.Contexts;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Application.Services.HomePage.Commands.AddNewSlider;

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

        #endregion
    }
}
