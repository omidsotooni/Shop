using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;
using static Shop.Common.Utility;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator, Customer")]
    public class OrdersController : Controller
    {
        #region Fields
        private readonly IFacadForSite _facadForSite;
        #endregion

        #region Constructor
        public OrdersController(IFacadForSite facadForSite)
        {
            _facadForSite = facadForSite;
        }
        #endregion

        #region Methods
        public IActionResult Index(OrderState orderState, int page = 1, int pageSize = 20)
        {
            return View(_facadForSite.GetOrdersServices.GetOrdersForAdmin(orderState, page, pageSize).Data);
        }

        #endregion
    }
}
