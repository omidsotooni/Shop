using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Application.Services.HomePage.Commands.AddNewSlider;
using Shop.Application.Services.HomePage.Commands.EditSliderService;
using Shop.Common.Dto;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
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
        public IActionResult Index(int Page = 1, int PageSize = 10)
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
        public IActionResult Edit(long Id)
        {
            var result = _facadForSite.EditSliderService.GetSlider(Id).Data; 
            if (result != null)
                return View(result);
            return View();
        }
        [HttpPost]
        public IActionResult Edit(EditSliderDto Slider)
        {
            if(Slider == null)
            {
                return Json(new ResultDto()
                {
                    IsSuccess = false,
                    Message = "برای ویرایش لطفا تغییرات اسلایدر مورد نظر را اعمال کنید.!",
                });
            }
            List<IFormFile> file = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var img = Request.Form.Files[i];
                file.Add(img);
            }
            var result = _facadForSite.EditSliderService.EditSlider(Slider, file.FirstOrDefault());
            return Json(result);
        }
        
        [HttpPost]
        public IActionResult ChangeStatusSlider(long SliderId)
        {
            var result = _facadForSite.SliderSatusChangeService.ChangeStatusSlider(SliderId);
            if (result != null)
                return Json(result);
            return View();
        }
        [HttpPost]
        public IActionResult Delete(long SliderId)
        {
            var result = _facadForSite.DeleteSliderService.DeleteSlider(SliderId);
            if (result != null)
                return Json(result);
            return View();
        }

        #endregion
    }
}
