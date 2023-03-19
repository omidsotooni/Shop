using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Application.Services.Products.Commands.AddNewProduct;
using Shop.Application.Services.Products.Commands.EditProduct;
using Shop.Common.Dto;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        #region Fields
        private readonly IProductFacad _productFacad;
        #endregion

        #region Constructor
        public ProductsController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }
        #endregion

        #region Methods
        public IActionResult Index(int Page = 1, int PageSize = 20)
        {
            return View(_productFacad.GetProductForAdminService.Execute(Page, PageSize).Data);
        }

        public IActionResult Detail(long Id)
        {
            return View(_productFacad.GetProductDetailForAdminService.Execute(Id).Data);
        }

        [HttpGet]
        public IActionResult AddNewProduct()
        {
            ViewBag.Categories = new SelectList(_productFacad.GetAllCategoriesService.Execute().Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult AddNewProduct(RequestAddNewProductDto request, List<AddNewProduct_Features> Features)
        {
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            request.Images = images;
            request.Features = Features;
            return Json(_productFacad.AddNewProductService.Execute(request));
        }

        [HttpPost]
        public IActionResult Delete(long ProductId)
        {
            return Json(_productFacad.RemoveProductService.Execute(ProductId));
        }

        public IActionResult Edit(long Id)
        {
            ViewBag.Categories = new SelectList(_productFacad.GetAllCategoriesService.Execute().Data, "Id", "Name");
            var result = _productFacad.EditProductService.GetProductById(Id).Data;
            if (result != null)
                return View(result);
            return View();
        }
        [HttpPost]
        public IActionResult Edit(EditProduct Product)
        {
            if (Product == null)
            {
                return Json(new ResultDto()
                {
                    IsSuccess = false,
                    Message = "برای ویرایش لطفا تغییرات محصول مورد نظر را اعمال کنید.!"
                });
            }
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            Product.MoreImages = images;
            var result = _productFacad.EditProductService.Execute(Product);
            return Json(result);
        }

        #endregion
    }
}
