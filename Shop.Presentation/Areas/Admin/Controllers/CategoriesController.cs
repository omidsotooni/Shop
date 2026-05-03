using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Common.Dto;
using Shop.Domain.Entities.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class CategoriesController : Controller
    {
        #region Fields
        private readonly IProductFacad _productFacad;
        #endregion

        #region Constructor
        public CategoriesController(IProductFacad productFacad, IServiceProvider serviceProvider)
        {
            _productFacad = productFacad;
        }
        #endregion

        #region Methods
        public IActionResult Index(long? parentId)
        {
            return View(_productFacad.GetCategoriesService.Execute(parentId).Data);
        }

        [HttpGet]
        public IActionResult AddNewCategory(long? parentId)
        {
            ViewBag.parentId = parentId;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewCategory(long? ParentId, string Name)
        {
            var result = _productFacad.AddNewCategoryService.Execute(ParentId, Name);
            return Json(result);
        }

        [HttpPost]
        public IActionResult Delete(long CategoryId)
        {
            return Json(_productFacad.RemoveCategoryService.Execute(CategoryId));
        }

        public ActionResult Edit(long CategoryId)
        {
            ViewBag.ParentCategory = new SelectList(_productFacad.GetCategoriesService.GetCategories().Data, "Id", "Name");
            return View(_productFacad.GetCategoriesService.GetCategoryById(CategoryId).Data);
        }
        [HttpPost]
        public IActionResult Edit(Category Item)
        {
            if (Item == null)
            {
                return Json(new ResultDto()
                {
                    IsSuccess = false,
                    Message = "برای ویرایش لطفا تغییرات دسته بندی مورد نظر را وارد نمائید.!"
                });
            }
            var result = _productFacad.EditCategoryService.Execute(Item);
            return Json(result);
        }
        #endregion
    }
}
