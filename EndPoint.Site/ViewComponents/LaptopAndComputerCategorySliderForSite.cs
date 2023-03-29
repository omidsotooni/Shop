using EndPoint.Site.Models.ViewModels.HomePages;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Application.Services.Products.Queries.GetProductForSite;

namespace EndPoint.Site.ViewComponents
{
    public class LaptopAndComputerCategorySliderForSite : ViewComponent
    {
        #region Fields
        private readonly IProductFacadForSite _productFacadForSite;
        #endregion

        #region Constructor
        public LaptopAndComputerCategorySliderForSite(IProductFacadForSite productFacadForSite)
        {
            _productFacadForSite = productFacadForSite;
        }
        #endregion

        #region Methods
        public IViewComponentResult Invoke()
        {
            long LaptopAndComputerCategoryId = 1;
            HomePageViewModel HomePageImages = new HomePageViewModel()
            {
                LaptopAndComputerCategory = _productFacadForSite.GetProductForSiteService.Execute(Ordering.theNewest, null, 1,
                10, LaptopAndComputerCategoryId).Data.Products,
            };
            if (HomePageImages != null)
            {
                return View(viewName: "LaptopAndComputerCategorySliderForSite", HomePageImages);
            }
            else
            {
                return View();
            }
        }
        #endregion
    }
}
