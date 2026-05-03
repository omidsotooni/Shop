using EndPoint.Site.Models.ViewModels.HomePages;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Application.Services.Products.FacadPattern;
using Shop.Application.Services.Products.Queries.GetProductForSite;

namespace EndPoint.Site.ViewComponents
{
    public class SlidersForSite : ViewComponent
    {
        #region Fields
        private readonly IFacadForSite _facadForSite;
        #endregion

        #region Constructor
        public SlidersForSite(IFacadForSite facadForSite)
        {
            _facadForSite = facadForSite;
        }
        #endregion

        #region Methods
        public IViewComponentResult Invoke()
        {
            HomePageViewModel HomePageSliders = new HomePageViewModel()
            {
                Sliders = _facadForSite.GetHomePageImageAndSlidersForSite.GetHomePageSliders().Data,
            };
            if (HomePageSliders != null)
            {
                return View(viewName: "SlidersForSite", HomePageSliders);
            }
            else
            {
                return View();
            }
        }
        #endregion
    }
}
