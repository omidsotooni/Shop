using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Application.Services.HomePage.Commands.AddNewSlider;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        #region Fields
        private readonly IFacadForSite _facadForSite;
        #endregion

        #region Constructor
        public SlidersController(IFacadForSite facadForSite)
        {
            _facadForSite = facadForSite;
        }
        #endregion

        #region Methods
        public IActionResult Index(int Page = 1, int PageSize = 20)
        {
            return View(_facadForSite.GetSliderForAdminService.GetSlidersForAdmin(Page, PageSize).Data);
        }
        public IActionResult AddNewSlider()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNewSlider(RequestAddNewSliderDto requestAdd)
        {

            List<IFormFile> file = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var img = Request.Form.Files[i];
                file.Add(img);
            }            
            return Json(_facadForSite.AddNewSliderService.Execute(requestAdd, file.First()));
        }

        #endregion
    }
}
