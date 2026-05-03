using EndPoint.Site.Models.ViewModels.HomePages;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;

namespace EndPoint.Site.ViewComponents
{
    public class PageImagesMainForSite : ViewComponent
    {
        #region Fields
        private readonly IFacadForSite _facadForSite;
        #endregion

        #region Constructor
        public PageImagesMainForSite(IFacadForSite facadForSite)
        {
            _facadForSite = facadForSite;
        }
        #endregion

        #region Methods
        public IViewComponentResult Invoke()
        {
            HomePageViewModel HomePageImages = new HomePageViewModel()
            {
                HomePageImagesDtos = _facadForSite.GetHomePageImageAndSlidersForSite.GetHomePageImages().Data,
            };
            if (HomePageImages != null)
            {
                return View(viewName: "PageImagesMainForSite", HomePageImages);
            }
            else
            {
                return View();
            }
        }
        #endregion
    }
}
