using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Application.Services.Products.Queries.GetProductForSite;

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
        public IActionResult Index(Ordering ordering, string SearchKey, long? CategoryId = null, int page = 1, int pageSize = 20)
        {
            return View(_productFacadForSite.GetProductForSiteService.Execute(ordering, SearchKey, page, pageSize, CategoryId).Data);
        }
        public IActionResult Detail(long Id)
        {
            return View(_productFacadForSite.GetProductDetailForSiteService.Execute(Id).Data);
        }
        
        #endregion
    }
}
