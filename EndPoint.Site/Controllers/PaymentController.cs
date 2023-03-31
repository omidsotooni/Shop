using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Common.Dto;

namespace EndPoint.Site.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        #region Fields
        private readonly IFacadForSite _facadForSite;
        private readonly CookiesManeger _cookiesManeger;
        #endregion

        #region Constructor
        public PaymentController(IFacadForSite facadForSite)
        {
            _facadForSite = facadForSite;
            _cookiesManeger = new CookiesManeger();
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            long? UserId = ClaimUtility.GetUserId(User);
            var cartItems = _facadForSite.CartService.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext), UserId);
            if (UserId != null && cartItems.Data.SumAmount > 0)
            {
                // Checking User's Cart
                var cart = _facadForSite.GetCartService.GetUserCart(UserId, cartItems.Data.CartId);
                if (!cart.IsSuccess)
                {
                    return Json(new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "سبد خرید متعلق به شما نیست!"
                    });
                }
                var requestPay = _facadForSite.PaymentServices.AddRequestPayment(cartItems.Data.SumAmount, UserId.Value);
                // Send to ZarinPal
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
        }

        #endregion
    }
}
