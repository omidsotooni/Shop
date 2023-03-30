using EndPoint.Site.Models.ViewModels.HomePages;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Application.Services.Products.Queries.GetProductForSite;

namespace EndPoint.Site.ViewComponents
{
    public class Cart : ViewComponent
    {
        #region Fields
        private readonly IFacadForSite _facadForSite;
        private readonly CookiesManeger _cookiesManeger;
        #endregion

        #region Constructor
        public Cart(IFacadForSite facadForSite)
        {
            _facadForSite = facadForSite;
            _cookiesManeger = new CookiesManeger();
        }
        #endregion

        #region Methods
        public IViewComponentResult Invoke()
        {
            var userId = ClaimUtility.GetUserId(HttpContext.User);
            var result = _facadForSite.CartService.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext), userId).Data;
            return View(viewName: "Cart", result);
        }
        #endregion
    }
}
