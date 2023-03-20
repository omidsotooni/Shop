using Shop.Application.Interfaces.Contexts;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Application.Services.Products.Queries.GetProductForSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this._context = context;
        }
        #endregion
        private IGetProductForSiteService _getProductForSiteService;
        public IGetProductForSiteService GetProductForSiteService
        {
            get
            {
                return _getProductForSiteService = _getProductForSiteService ?? new GetProductForSiteService(_context);
            }


            #region Methods
        }
    }
