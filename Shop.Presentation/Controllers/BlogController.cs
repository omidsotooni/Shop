using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.FacadPatterns;

namespace EndPoint.Site.Controllers
{
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
        public IActionResult Index(string SearchKey, int Page = 1, int PageSize = 20)
        {
            return View(_facadForSite.GetBlogServices.GetBlogsForSite(SearchKey, Page, PageSize).Data);
        }

        [Route("/Blog/{slug}")]
        public IActionResult Article(string Slug)
        {
            if (Slug == null)
            {
                return RedirectToAction("NotFound404", "Home");
            }
            var res = _facadForSite.GetBlogServices.GetBlogBySlug(Slug).Data;
            if (res == null)
            {
                return RedirectToAction("NotFound404", "Home");
            }
            return View(res);
        }
        #endregion
    }
}
