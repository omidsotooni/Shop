using Shop.Application.Interfaces.Contexts;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Application.Services.Common.Queries.GetCategory;
using Shop.Application.Services.Common.Queries.GetMenuItem;
using Shop.Application.Services.Products.Queries.GetProductDetailForSite;
using Shop.Application.Services.Products.Queries.GetProductForSite;

namespace Shop.Application.Services.Products.FacadPattern
{
    public class ProductFacadForSite : IProductFacadForSite
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public ProductFacadForSite(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        private IGetProductForSiteService _getProductForSiteService;
        public IGetProductForSiteService GetProductForSiteService
        {
            get
            {
                return _getProductForSiteService = _getProductForSiteService ?? new GetProductForSiteService(_context);
            }
        }

        private IGetProductDetailForSiteService _getProductDetailForSiteService;
        public IGetProductDetailForSiteService GetProductDetailForSiteService
        {
            get
            {
                return _getProductDetailForSiteService = _getProductDetailForSiteService ?? new GetProductDetailForSiteService(_context);
            }
        }

        private IGetMenuItemService _getMenuItemService;
        public IGetMenuItemService GetMenuItemService
        {
            get
            {
                return _getMenuItemService = _getMenuItemService ?? new GetMenuItemService(_context);
            }
        }

        private IGetCategoryService _getCategoryService;
        public IGetCategoryService GetCategoryService
        {
            get
            {
                return _getCategoryService = _getCategoryService ?? new GetCategoryService(_context);
            }
        }
        
        #endregion
    }
}
