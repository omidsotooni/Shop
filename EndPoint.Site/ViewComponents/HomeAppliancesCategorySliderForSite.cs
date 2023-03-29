using EndPoint.Site.Models.ViewModels.HomePages;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Application.Services.Products.Queries.GetProductForSite;

namespace EndPoint.Site.ViewComponents
{
    public class HomeAppliancesCategorySliderForSite : ViewComponent
    {
        #region Fields
        private readonly IProductFacadForSite _productFacadForSite;
        #endregion

        #region Constructor
        public HomeAppliancesCategorySliderForSite(IProductFacadForSite productFacadForSite)
        {
            _productFacadForSite = productFacadForSite;
        }
        #endregion

        #region Methods
        public IViewComponentResult Invoke()
        {
            long HomeAppliancesCategoryId = 1;
            HomePageViewModel HomePageImages = new HomePageViewModel()
            {
                HomeAppliancesCategory = _productFacadForSite.GetProductForSiteService.Execute(Ordering.theNewest, null, 1,
                10, HomeAppliancesCategoryId).Data.Products,
            };
            if (HomePageImages != null)
            {
                return View(viewName: "HomeAppliancesCategorySliderForSite", HomePageImages);
            }
            else
            {
                return View();
            }
        }
        #endregion
    }
}
