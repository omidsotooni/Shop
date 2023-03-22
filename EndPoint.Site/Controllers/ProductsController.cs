using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Interfaces.FacadPatterns;

namespace EndPoint.Site.Controllers
{
    public class ProductsController : Controller
    {
        #region Fields
        private readonly IProductFacadForSite _productFacadForSite;
        #endregion
        #region Constructor
        public ProductsController(IProductFacadForSite productFacadForSite)
        {
            _productFacadForSite = productFacadForSite;
        }

        #endregion

        #region Methods
        public IActionResult Index(int page = 1, long? CategoryId = null)
        {
            return View(_productFacadForSite.GetProductForSiteService.Execute(page, CategoryId).Data);
        }
        public IActionResult Detail(long Id)
        {
            return View(_productFacadForSite.GetProductDetailForSiteService.Execute(Id).Data);
        }
        
        #endregion
    }
}
