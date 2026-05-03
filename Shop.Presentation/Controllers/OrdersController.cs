using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;

namespace EndPoint.Site.Controllers
{
    [Authorize]
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
        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            long UserId = ClaimUtility.GetUserId(User).Value;            
            return View(_facadForSite.GetOrdersServices.GetOrdersForUser(UserId, page, pageSize).Data);
        }

        #endregion
    }
}
