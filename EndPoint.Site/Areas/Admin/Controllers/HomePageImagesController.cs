using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Application.Services.HomePage.Commands.HomePageImagesService;
using Shop.Common.Dto;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
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
        public IActionResult Index(int Page = 1, int PageSize = 10)
        {
            return View(_facadForSite.GetHomePageImagesService.GetHomePageImagesForAdmin(Page, PageSize).Data);
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
        public IActionResult Edit(long Id)
        {
            var result = _facadForSite.HomePageImagesService.GetHomePageImageForEdit(Id).Data;
            if (result != null)
                return View(result);
            return View();
        }
        [HttpPost]
        public IActionResult Edit(RequestEditHomePageImagesDto request)
        {
            if (request == null)
            {
                return Json(new ResultDto()
                {
                    IsSuccess = false,
                    Message = "برای ویرایش لطفا تغییرات تصویر مورد نظر را اعمال کنید.!",
                });
            }
            List<IFormFile> file = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var img = Request.Form.Files[i];
                file.Add(img);
            }
            if (file != null && file.Count() > 0)
            {
                request.file = file.First();
            }
            var result = _facadForSite.HomePageImagesService.EditHomePageImage(request);
            if (result != null)
                return Json(result);
            return View();
        }

        [HttpPost]
        public IActionResult ChangeStatusHomePageImage(long HomePageImageId)
        {
            var result = _facadForSite.HomePageImagesService.ChangeStatusHomePageImage(HomePageImageId);
            if (result != null)
                return Json(result);
            return View();
        }
        [HttpPost]
        public IActionResult Delete(long HomePageImageId)
        {
            var result = _facadForSite.HomePageImagesService.DeleteHomePageImage(HomePageImageId);
            if (result != null)
                return Json(result);
            return View();
        }

        #endregion
    }
}
