using EndPoint.Site.Models.ViewModels.HomePages;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Application.Services.Products.Queries.GetProductForSite;
using Shop.Common;

namespace EndPoint.Site.ViewComponents
{
    public class MobileCategorySliderForSite : ViewComponent
    {
        #region Fields
        private readonly IProductFacadForSite _productFacadForSite;
        private readonly long MobileCategoryId = ConstString.MobileCategoryId;
        #endregion

        #region Constructor
        public MobileCategorySliderForSite(IProductFacadForSite productFacadForSite)
        {
            _productFacadForSite = productFacadForSite;
        }
        #endregion

        #region Methods
        public IViewComponentResult Invoke()
        {
            HomePageViewModel HomePageImages = new HomePageViewModel()
            {
                MobileCategory = _productFacadForSite.GetProductForSiteService.Execute(Ordering.theNewest, null, 1,
                10, MobileCategoryId).Data.Products,
            };
            if (HomePageImages != null)
            {
                return View(viewName: "MobileCategorySliderForSite", HomePageImages);
            }
            else
            {
                return View();
            }
        }
        #endregion
    }
}
