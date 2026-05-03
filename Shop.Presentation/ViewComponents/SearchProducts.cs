using Shop.Application.Interfaces.FacadPatterns;
using Microsoft.AspNetCore.Mvc;
using EndPoint.Site.Models.ViewModels.ProductsViewModel;

namespace EndPoint.Site.ViewComponents
{
    public class SearchProducts : ViewComponent
    {
        private readonly IProductFacadForSite _productFacadForSite;
        public SearchProducts(IProductFacadForSite productFacadForSite)
        {
            _productFacadForSite = productFacadForSite;
        }
        public IViewComponentResult Invoke()
        {
            return View(viewName: "SearchProducts", _productFacadForSite.GetCategoryService.Execute().Data);
        }
    }
}
