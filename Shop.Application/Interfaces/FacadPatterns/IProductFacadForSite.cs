using Shop.Application.Services.Products.Queries.GetProductForSite;

namespace Shop.Application.Interfaces.FacadPatterns
{
    public interface IProductFacadForSite
    {
        /// <summary>
        /// Get Products For Site
        /// </summary>
        IGetProductForSiteService GetProductForSiteService { get; }
    }
}
