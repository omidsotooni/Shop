using EndPoint.Site.Models.ViewModels.HomePages;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Domain.Entities.HomePages;

namespace EndPoint.Site.ViewComponents
{
    public class PageImagesForSite : ViewComponent
    {
        #region Fields
        private readonly IFacadForSite _facadForSite;
        #endregion

        #region Constructor
        public PageImagesForSite(IFacadForSite facadForSite)
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
                return View(viewName: "PageImagesForSite", HomePageImages);
            }
            else
            {
                return View();
            }
        }
        #endregion
    }
}
