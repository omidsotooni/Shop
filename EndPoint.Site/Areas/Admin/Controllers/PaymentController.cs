using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PaymentController : Controller
    {
        #region Fields
        private readonly IFacadForSite _facadForSite;
        #endregion

        #region Constructor
        public PaymentController(IFacadForSite facadForSite)
        {
            _facadForSite = facadForSite;
        }
        #endregion

        #region Methods
        public IActionResult Index(int Page = 1, int PageSize = 10)
        {
            return View(_facadForSite.GetPaymentServices.GetPaymentForAdmin(Page, PageSize).Data);
        }
        #endregion
    }
}
