
using EndPoint.Site.Models.ZarinpalModels;
using EndPoint.Site.Utilities;
using EndPoint.Site.Zarinpal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Common;
using Shop.Common.Dto;

namespace EndPoint.Site.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        #region Fields
        private readonly IFacadForSite _facadForSite;
        private readonly CookiesManeger _cookiesManeger;
        private readonly string MerchantIdZarinpal = ConstString.MerchantIdZarinpal;
        private readonly string MerchantIdIDPay = ConstString.MerchantIdIDPay;
        private readonly string CallbackUrl = ConstString.CallBackurl;
        #endregion

        #region Constructor
        public PaymentController(IFacadForSite facadForSite)
        {
            _facadForSite = facadForSite;
            _cookiesManeger = new CookiesManeger();
        }
        #endregion

        #region Methods
        public IActionResult Index(Banking banking)
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
                switch (banking)
                {
                    case Banking.Zarinpal:
                        // Send to ZarinPal               
                        var request = SendToZarinpal(requestPay.Data.Amount, MerchantIdZarinpal, requestPay.Data.PaymentGuid,
                            requestPay.Data.RequestPaymentId, CallbackUrl, requestPay.Data.Email, "09011234567");
                        var response = ZarinpalRestApi.Payment(request);
                        if (response.Data.Code == 100)
                        {
                            var gatewayLink = ZarinpalRestApi.GenerateGatewayLink(response.Data.Authority);
                            return Redirect(gatewayLink);
                        }
                        TempData["Message"] = response.Data.Code;
                        break;
                    case Banking.IDPay:
                        break;
                    default:
                        break;
                }
                return View("Index");
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
        }

        public IActionResult VerifyZarinpal(Guid PaymentGuid, string authority, string status)
        {
            var viewModel = new ResultDto();
            if (status == "NOK")
            {
                viewModel.IsSuccess = false;
                viewModel.Message = "پرداخت ناموفق!";
            }
            else if (status == "OK")
            {
                var requestPay = _facadForSite.GetPaymentServices.GetPayment(PaymentGuid);
                var request = new VerifyRequest
                {
                    MerchantId = MerchantIdZarinpal,
                    Authority = authority,
                    Amount = requestPay.Data.Amount * 10
                };
                var response = ZarinpalRestApi.Verify(request);
                if (response.Data.Code == 100) // Successful
                {
                    viewModel.IsSuccess = true;
                    viewModel.Message = $"پرداخت موفق، شماره تراکنش: {response.Data.RefId}";
                }
                else if (response.Data.Code == 101) // Repeated successful
                {
                    viewModel.IsSuccess = true;
                    viewModel.Message = $"این تراکنشن قبلا با موفقیت انجام شده است، شماره تراکنش: {response.Data.RefId}";
                }
                else // Error
                {
                    viewModel.IsSuccess = false;
                    viewModel.Message = $"پرداخت ناموفق! کد خطا: {response.Data.Code}";
                }
            }
            return RedirectToAction("ResultPayment", viewModel);
        }
        public IActionResult ResultPayment(ResultDto viewModel)
        {
            return View(viewModel);
        }
        public PaymentRequest SendToZarinpal(int Amount, string MerchantId, Guid PaymentGuid, long RequestPaymentId, string Callback, string Email, string Mobile)
        {
            var request = new PaymentRequest
            {
                MerchantId = MerchantId,
                Amount = Amount * 10,
                CallbackUrl = $"{Request.Scheme}://{Request.Host}/{Callback}{PaymentGuid}",
                Description = "پرداخت فاکتور شماره: " + RequestPaymentId,
                Metadata = new PaymentRequestMetadata // Either none, or all
                {
                    Email = Email,
                    Mobile = Mobile
                }
            };
            return request;
        }
        public enum Banking
        {
            /// <summary>
            /// Zarinpal
            /// </summary>
            Zarinpal = 0,
            /// <summary>
            /// IDPay
            /// </summary>
            IDPay = 1,            
        }
        #endregion
    }
}
