using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Domain.Entities.Carts;

namespace EndPoint.Site.Controllers
{
    public class CartController : Controller
    {
        #region Fields
        private readonly IFacadForSite _facadForSite;
        private readonly CookiesManeger cookiesManeger;
        #endregion

        #region Constructor
        public CartController(IFacadForSite facadForSite)
        {
            _facadForSite = facadForSite;
            cookiesManeger = new CookiesManeger();
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            var userId = ClaimUtility.GetUserId(User);
            var resultGetLst = _facadForSite.GetCartService.GetMyCart(cookiesManeger.GetBrowserId(HttpContext), userId).Data;
            if(resultGetLst != null)
            {
                return View(resultGetLst);
            }
            return View();
        }
        public IActionResult AddToCart(long ProductId)
        {
            _facadForSite.CartService.AddToCart(ProductId, cookiesManeger.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
        }
        public IActionResult Add(long CartItemId)
        {
            _facadForSite.CartService.Add(CartItemId);
            return RedirectToAction("Index");
        }
        public IActionResult LowOff(long CartItemId)
        {
            _facadForSite.CartService.LowOff(CartItemId);
            return RedirectToAction("Index");
        }
        public IActionResult Remove(long ProductId)
        {
            _facadForSite.CartService.RemoveFromCart(ProductId, cookiesManeger.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
        }

        #endregion
    }
}
