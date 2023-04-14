using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Common;
using Shop.Common.Dto;
using Shop.Domain.Entities.Blog;
using Shop.Infrastructure.Dtos.BlogDtos;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Author")]
    public class BlogController : Controller
    {
        #region Fields
        private readonly IFacadForSite _facadForSite;
        #endregion

        #region Constructor
        public BlogController(IFacadForSite facadForSite)
        {
            _facadForSite = facadForSite;
        }
        #endregion
        #region Methods
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddNewBlog()
        {
            ViewBag.UserId = ClaimUtility.GetUserId(User);
            ViewBag.BlogCategories = new SelectList(_facadForSite.GetBlogServices.GetAllBlogCategories().Data, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult AddNewBlog(List<FAQBlogList> fAQBlog, IFormCollection blogValuePairs)
        {
            AddNewBlogDto addNewBlogDto = new AddNewBlogDto();
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            addNewBlogDto.BlogImages = images;
            addNewBlogDto.FAQBlogs = fAQBlog;
            addNewBlogDto.Content = blogValuePairs["Content"];
            addNewBlogDto.UrlRedirect = blogValuePairs["UrlRedirect"];
            addNewBlogDto.VideoUrl = blogValuePairs["VideoUrl"];
            addNewBlogDto.Canonical = blogValuePairs["Canonical"];
            addNewBlogDto.IsIndexed = blogValuePairs["IsIndexed"].Contains("true");
            addNewBlogDto.IsFollowed = blogValuePairs["IsFollowed"].Contains("true");
            addNewBlogDto.ReadingTime = int.Parse(blogValuePairs["ReadingTime"]);
            addNewBlogDto.LanguageId = long.Parse(blogValuePairs["LanguageId"]);
            addNewBlogDto.BlogStatus = ((Utility.BlogStatus)int.Parse(blogValuePairs["BlogStatus"]));
            addNewBlogDto.BlogCategoryId = long.Parse(blogValuePairs["BlogCategoryId"]);
            addNewBlogDto.Description = blogValuePairs["Description"];
            addNewBlogDto.Slug = blogValuePairs["Slug"];
            addNewBlogDto.Title = blogValuePairs["Title"];
            addNewBlogDto.UserId = long.Parse(blogValuePairs["UserId"]);
            addNewBlogDto.Tags = blogValuePairs["Tags"];
            
            return Json(_facadForSite.BlogServices.AddNewBlog(addNewBlogDto));
        }
        public IActionResult IndexBlogCategories()
        {
            return View(_facadForSite.GetBlogServices.GetAllBlogCategories().Data);
        }
        [HttpGet]
        public IActionResult AddNewBlogCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNewBlogCategory(string CategoryText)
        {
            var result = _facadForSite.BlogServices.AddNewBlogCategory(CategoryText);
            return Json(result);
        }
        [HttpPost]
        public IActionResult DeleteBlogCategory(long blogCategoryId)
        {
            return Json(_facadForSite.BlogServices.DeleteBlogCategory(blogCategoryId));
        }
        public ActionResult EditBlogCategory(long blogCategoryId)
        {
            return View(_facadForSite.GetBlogServices.GetBlogCategory(blogCategoryId).Result.Data);
        }
        [HttpPost]
        public IActionResult EditBlogCategory(BlogCategory Item)
        {
            if (Item == null)
            {
                return Json(new ResultDto()
                {
                    IsSuccess = false,
                    Message = "برای ویرایش لطفا تغییرات دسته بندی مورد نظر را وارد نمائید.!"
                });
            }
            var result = _facadForSite.BlogServices.EditBlogCategory(Item);
            return Json(result);
        }

        #endregion
    }
}
