
using EndPoint.Site.Models.ZarinpalModels;
using EndPoint.Site.Utilities;
using EndPoint.Site.RestApis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Common;
using Shop.Common.Dto;
using EndPoint.Site.Models.IDPayModels;
using Shop.Domain.Entities.Finances;
using RestSharp.Extensions;
using static Shop.Common.Utility;
using NuGet.Configuration;
using Shop.Application.Services.Orders.Commands;
using Shop.Domain.Entities.Users;

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
        private readonly string CallbackUrl = ConstString.CallBackurlZarinpal;
        private readonly string CallBackurlIDPay = ConstString.CallBackurlIDPay;

        #endregion

        #region Constructor
        public PaymentController(IFacadForSite facadForSite)
        {
            _facadForSite = facadForSite;
            _cookiesManeger = new CookiesManeger();
        }
        #endregion

        #region Methods
        public async Task<IActionResult> Index(Banking banking)
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
                string Description = "پرداخت فاکتور شماره: ";
                var requestPay = _facadForSite.PaymentServices.AddRequestPayment(cartItems.Data.SumAmount, UserId.Value);
                switch (banking)
                {
                    case Banking.Zarinpal:
                        // Send to ZarinPal
                        var request = SendToZarinpal(requestPay.Data.Amount, MerchantIdZarinpal, requestPay.Data.PaymentGuid,
                            requestPay.Data.RequestPaymentId, CallbackUrl, requestPay.Data.Email, "09011234567", Description);
                        var response = ZarinpalRestApi.Payment(request);
                        if (response.Data.Code == 100)
                        {
                            var gatewayLink = ZarinpalRestApi.GenerateGatewayLink(response.Data.Authority);
                            return Redirect(gatewayLink);
                        }
                        TempData["Message"] = response.Data.Code;
                        break;
                    case Banking.IDPay:
                        var requestIDPay = SendToIDPay("فروشگاه", requestPay.Data.Amount, requestPay.Data.PaymentGuid, requestPay.Data.RequestPaymentId
                            , requestPay.Data.Email, "09011234567", Description, CallBackurlIDPay);
                        IDPayRestApi idpaypayRestApi = new IDPayRestApi();
                        var responseIDPay = await idpaypayRestApi.PaymentIDPay(requestIDPay);
                        if (responseIDPay != null && responseIDPay.Status == 201)
                        {
                            RequestResponsSuccess? responseData = (responseIDPay.Data as RequestResponsSuccess);                            
                            return Redirect(responseData.Link);
                        }
                        break;
                    default:
                        break;
                }
                return RedirectToAction("Index", "Orders");
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
        }

        public IActionResult VerifyZarinpal(Guid PaymentGuid, string authority, string status)
        {
            long? UserId = ClaimUtility.GetUserId(User);
            var cart = _facadForSite.CartService.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext), UserId);
            var paymentReq = _facadForSite.GetPaymentServices.GetPayment(PaymentGuid);

            var viewModel = new ResultDto();
            if (status == "NOK")
            {
                viewModel.IsSuccess = false;
                viewModel.Message = "پرداخت ناموفق!";
                _facadForSite.OrderServices.AddNewOrder(new RequestAddNewOrderSericeDto
                {
                    CartId = cart.Data.CartId,
                    UserId = UserId.Value,
                    PaymentId = paymentReq.Data.Id,
                    Status = false,
                    Address = "User Address",
                    BankingGateway = Banking.Zarinpal,
                    Authority = "",
                    RefId = 0,
                });
            }
            else if (status == "OK")
            {
                var request = new VerifyRequest
                {
                    MerchantId = MerchantIdZarinpal,
                    Authority = authority,
                    Amount = paymentReq.Data.Amount * 10
                };
                var response = ZarinpalRestApi.Verify(request);
                if (response.Data.Code == 100) // Successful
                {
                    viewModel.IsSuccess = true;
                    viewModel.Message = $"پرداخت موفق، شماره تراکنش: {response.Data.RefId}";
                    _facadForSite.OrderServices.AddNewOrder(new RequestAddNewOrderSericeDto
                    {
                        CartId = cart.Data.CartId,
                        UserId = UserId.Value,
                        PaymentId = paymentReq.Data.Id,
                        Status = true,
                        Address = "User Address",
                        BankingGateway = Banking.Zarinpal,
                        Authority = authority,
                        RefId = response.Data.RefId,
                    });
                    //redirect to orders
                    return RedirectToAction("Index", "Orders");
                }
                else if (response.Data.Code == 101) // Repeated successful
                {
                    viewModel.IsSuccess = true;
                    viewModel.Message = $"این تراکنشن قبلا با موفقیت انجام شده است، شماره تراکنش: {response.Data.RefId}";
                    return RedirectToAction("Index", "Orders");
                }
                else // Error
                {
                    viewModel.IsSuccess = false;
                    viewModel.Message = $"پرداخت ناموفق! کد خطا: {response.Data.Code}";
                    _facadForSite.OrderServices.AddNewOrder(new RequestAddNewOrderSericeDto
                    {
                        CartId = cart.Data.CartId,
                        UserId = UserId.Value,
                        PaymentId = paymentReq.Data.Id,
                        Status = false,
                        Address = "User Address",
                        BankingGateway = Banking.Zarinpal,
                        Authority = "",
                        RefId = 0,
                    });
                    return RedirectToAction("Index", "Orders");
                }
            }
            return RedirectToAction("ResultPayment", viewModel);
        }
        public IActionResult VerifyIDPay(Guid PaymentGuid)
        {
            long? UserId = ClaimUtility.GetUserId(User);
            var cart = _facadForSite.CartService.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext), UserId);
            var paymentReq = _facadForSite.GetPaymentServices.GetPayment(PaymentGuid);
            var viewModel = new ResultDto();
            var request = new ResultPaymentIDPay();
            try
            {
                request = new ResultPaymentIDPay
                {
                    status = int.Parse(Request.Query["status"]),
                    track_id = Request.Query["track_id"],
                    id = Request.Query["id"],
                    order_id = Request.Query["order_id"],
                    amount = decimal.Parse(Request.Query["amount"]),
                    card_no = Request.Query["card_no"],
                    hashed_card_no = Request.Query["hashed_card_no"],
                    date = double.Parse(Request.Query["date"]),
                };
            }
            catch (Exception)
            {
                request = new ResultPaymentIDPay
                {
                    status = int.Parse("0"),
                    track_id = "track_id",
                    id = "id",
                    order_id = "order_id",
                    amount = decimal.Parse("0"),
                    card_no = "card_no",
                    hashed_card_no = "hashed_card_no",
                    date = double.Parse("0"),
                };
                request.IsOK = false;
            }
            if (!request.IsOK)
            {
                ViewBag.ID = request.id;
                ViewBag.OrderID = request.order_id;
                viewModel.Message = request.Message;
                viewModel.IsSuccess = false;
                _facadForSite.OrderServices.AddNewOrder(new RequestAddNewOrderSericeDto
                {
                    CartId = cart.Data.CartId,
                    UserId = UserId.Value,
                    PaymentId = paymentReq.Data.Id,
                    Status = false,
                    Address = "User Address",
                    BankingGateway = Banking.IDPay,
                    Authority = request.order_id,
                    RefId = 0,
                });
                //redirect to orders
                return RedirectToAction("Index", "Orders");
            }
            else
            {
                // تایید تراکنش
                IDPayRestApi idpayRestApi = new IDPayRestApi();
                var res = idpayRestApi.VerifyIDPay(request, MerchantIdIDPay).Result;
                if (res is PaymentInfoIDPay)
                {
                    viewModel.Message = request.Message;
                    viewModel.IsSuccess = false;
                    _facadForSite.OrderServices.AddNewOrder(new RequestAddNewOrderSericeDto
                    {
                        CartId = cart.Data.CartId,
                        UserId = UserId.Value,
                        PaymentId = paymentReq.Data.Id,
                        Status = false,
                        Address = "User Address",
                        BankingGateway = Banking.IDPay,
                        Authority = request.order_id,
                        RefId = 0,
                    });
                    //redirect to orders
                    return RedirectToAction("Index", "Orders");
                }
                else
                {
                    viewModel.Message = request.Message;
                    viewModel.IsSuccess = true;
                    ViewBag.ID = res.id;
                    ViewBag.OrderID = res.order_id;
                    _facadForSite.OrderServices.AddNewOrder(new RequestAddNewOrderSericeDto
                    {
                        CartId = cart.Data.CartId,
                        UserId = UserId.Value,
                        PaymentId = paymentReq.Data.Id,
                        Status = true,
                        Address = "User Address",
                        BankingGateway = Banking.IDPay,
                        Authority = res.order_id,
                        RefId = long.Parse(res.track_id),
                    });
                    //redirect to orders
                    return RedirectToAction("Index", "Orders");
                }
            }
        }
        public IActionResult ResultPayment(ResultDto viewModel)
        {
            return View(viewModel);
        }
        public PaymentRequest SendToZarinpal(int Amount, string MerchantId, Guid PaymentGuid, long RequestPaymentId, string Callback, string Email, string Mobile, string Description)
        {
            var request = new PaymentRequest
            {
                MerchantId = MerchantId,
                Amount = Amount * 10,
                CallbackUrl = $"{Request.Scheme}://{Request.Host}/{Callback}{PaymentGuid}",
                Description = Description + RequestPaymentId,
                Metadata = new PaymentRequestMetadata // Either none, or all
                {
                    Email = Email,
                    Mobile = Mobile
                }
            };
            return request;
        }
        public RequestIDPay SendToIDPay(string Name, int Amount, Guid PaymentGuid, long RequestPaymentId, string Email, string Mobile, string Description, string CallBack)
        {
            string GuidPayment = PaymentGuid.ToString();
            var request = new RequestIDPay()
            {
                Amount = Amount,
                Desc = Description + RequestPaymentId,
                Mail = Email,
                Phone = Mobile,
                Name = Name,
                OrderId = RequestPaymentId.ToString(),
                Callback = $"{Request.Scheme}://{Request.Host}/{CallBack}{GuidPayment}",
            };
            return request;
        }

        #endregion
    }
}
