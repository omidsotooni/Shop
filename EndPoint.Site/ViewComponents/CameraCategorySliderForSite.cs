using EndPoint.Site.Models.ViewModels.HomePages;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Application.Services.Products.Queries.GetProductForSite;
using Shop.Common;

namespace EndPoint.Site.ViewComponents
{
    public class CameraCategorySliderForSite : ViewComponent
    {
        #region Fields
        private readonly IProductFacadForSite _productFacadForSite;
        private readonly long CameraSliderForSiteId = ConstString.CameraSliderForSiteId;
        #endregion

        #region Constructor
        public CameraCategorySliderForSite(IProductFacadForSite productFacadForSite)
        {
            _productFacadForSite = productFacadForSite;
        }
        #endregion

        #region Methods
        public IViewComponentResult Invoke()
        {
            HomePageViewModel HomePageImages = new HomePageViewModel()
            {
                CameraCategory = _productFacadForSite.GetProductForSiteService.Execute(Ordering.theNewest, null, 1,
                10, CameraSliderForSiteId).Data.Products,
            };
            if (HomePageImages != null)
            {
                return View(viewName: "CameraCategorySliderForSite", HomePageImages);
            }
            else
            {
                return View();
            }
        }
        #endregion
    }
}
