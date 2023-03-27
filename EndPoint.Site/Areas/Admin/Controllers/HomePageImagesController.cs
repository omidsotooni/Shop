using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Application.Services.HomePage.Commands.HomePageImagesService;
using Shop.Common.Dto;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageImagesController : Controller
    {
        #region Fields
        private readonly IFacadForSite _facadForSite;
        #endregion

        #region Constructor
        public HomePageImagesController(IFacadForSite facadForSite)
        {
            _facadForSite = facadForSite;
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(RequestHomePageImagesDto request)
        {
            List<IFormFile> file = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var img = Request.Form.Files[i];
                file.Add(img);
            }
            if (file == null)
            {
                return Json(new ResultDto
                {
                    IsSuccess = false,
                    Message = "لطفا تصویر را وارد نمائید.",
                });
            }
            request.file = file.First();
            var result = _facadForSite.HomePageImagesService.AddHomePageImages(request);
            if (result != null)
                return Json(result);
            return View();
        }

        #endregion
    }
}
