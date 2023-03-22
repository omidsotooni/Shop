using Shop.Application.Services.Common.Queries.GetMenuItem;
using Shop.Application.Services.Products.Queries.GetProductDetailForSite;
using Shop.Application.Services.Products.Queries.GetProductForSite;

namespace Shop.Application.Interfaces.FacadPatterns
{
    public interface IProductFacadForSite
    {
        /// <summary>
        /// Get Products For Site
        /// </summary>
        IGetProductForSiteService GetProductForSiteService { get; }
        /// <summary>
        /// Get Product Details For Site
        /// </summary>
        public IGetProductDetailForSiteService GetProductDetailForSiteService { get; }
        /// <summary>
        /// Get Menu Items For site
        /// </summary>
        public IGetMenuItemService GetMenuItemService { get; }
    }
}
