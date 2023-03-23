using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Contexts;
using Shop.Common;
using Shop.Common.Dto;

namespace Shop.Application.Services.Products.Queries.GetProductForSite
{
    public class GetProductForSiteService : IGetProductForSiteService
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public GetProductForSiteService(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public ResultDto<ResultProductForSiteDto> Execute(Ordering ordering, string SearchKey, int Page, int pageSize, long? CategoryId)
        {
            try
            {
                int totalRow = 0;
                var productQuery = _context.Products.Where(o => o.Displayed).
                    Include(o => o.ProductImages).AsQueryable();
                if (CategoryId != null)
                {
                    productQuery = productQuery.Where(p => p.CategoryId == CategoryId || p.Category.ParentCategoryId == CategoryId).AsQueryable();
                }
                if (!string.IsNullOrWhiteSpace(SearchKey))
                {
                    productQuery = productQuery.Where(p => p.Name.Contains(SearchKey) || p.Brand.Contains(SearchKey)).AsQueryable();
                }

                switch (ordering)
                {
                    case Ordering.NotOrder:
                        productQuery = productQuery.OrderByDescending(o => o.Id).AsQueryable();
                        break;
                    case Ordering.MostVisited:
                        productQuery = productQuery.OrderByDescending(o => o.ViewCount).AsQueryable();
                        break;
                    case Ordering.Bestselling:
                        break;
                    case Ordering.MostPopular:
                        break;
                    case Ordering.theNewest:
                        productQuery = productQuery.OrderByDescending(o => o.Id).AsQueryable();
                        break;
                    case Ordering.TheCheapest:
                        productQuery = productQuery.OrderBy(o => o.Price).AsQueryable();
                        break;
                    case Ordering.theMostExpensive:
                        productQuery = productQuery.OrderByDescending(o => o.Price).AsQueryable();
                        break;
                    default:
                        productQuery = productQuery.OrderByDescending(o => o.Id).AsQueryable();
                        break;
                }

                var product = productQuery.ToPaged(Page, pageSize, out totalRow);
                Random random = new Random();
                return product == null ? throw new Exception("Product Not Found !")
                    : new ResultDto<ResultProductForSiteDto>
                    {
                        Data = new ResultProductForSiteDto
                        {
                            TotalRow = totalRow,
                            Products = product.Select(o => new ProductForSiteDto
                            {
                                Id = o.Id,
                                Star = random.Next(1, 5),
                                Title = o.Name,
                                ImageSrc = o.ProductImages.FirstOrDefault().Src,
                                Price = o.Price
                            }).ToList(),
                        },
                        IsSuccess = true,
                    };
            }
            catch (Exception ex)
            {
                string str = "Error From Server: ";
                if (!string.IsNullOrEmpty(ex.Message))
                    str += ex.Message;
                return new ResultDto<ResultProductForSiteDto>
                {
                    IsSuccess = false,
                };
            }
        }

        #endregion
    }

    public class ResultProductForSiteDto
    {
        public List<ProductForSiteDto> Products { get; set; }
        public int TotalRow { get; set; }
    }

    public class ProductForSiteDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ImageSrc { get; set; }
        public int Star { get; set; }
        public int Price { get; set; }
    }
}
