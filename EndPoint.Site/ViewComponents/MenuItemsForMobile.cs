using Shop.Application.Interfaces.FacadPatterns;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.ViewComponents
{
    public class MenuItemsForMobile : ViewComponent
    {
        private readonly IProductFacadForSite _productFacadForSite;
        public MenuItemsForMobile(IProductFacadForSite productFacadForSite)
        {
            _productFacadForSite = productFacadForSite;
        }
        public IViewComponentResult Invoke()
        {
            var menuItemForMobile = _productFacadForSite.GetMenuItemService.GetMenuItemForMobile();
            return View(viewName: "MenuItemsForMobile", menuItemForMobile.Data);
        }
    }
}
