using Shop.Application.Interfaces.FacadPatterns;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.ViewComponents
{
    public class MenuItems : ViewComponent
    {
        private readonly IProductFacadForSite _productFacadForSite;
        public MenuItems(IProductFacadForSite productFacadForSite)
        {
            _productFacadForSite = productFacadForSite;
        }
        public IViewComponentResult Invoke()
        {
            var menuItem = _productFacadForSite.GetMenuItemService.Execute();
            return View(viewName: "MenuItems", menuItem.Data);
        }
    }
}
