using Shop.Application.Interfaces.Contexts;
using Microsoft.EntityFrameworkCore;
using Shop.Common.Dto;

namespace Shop.Application.Services.Products.Queries.GetProductDetailForSite
{
    public class GetProductDetailForSiteService : IGetProductDetailForSiteService
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public GetProductDetailForSiteService(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public ResultDto<ProductDetailForSiteDto> Execute(long Id)
        {
            try
            {
                var Product = _context.Products
                    .Include(o => o.Category).ThenInclude(o => o.ParentCategory)
                    .Include(o => o.ProductImages).Include(o => o.ProductFeatures)
                    .Where(o => o.Id == Id && o.Displayed).FirstOrDefault();
                Random random = new Random();
                return Product == null ? throw new Exception("Product Not Found !")
                    : new ResultDto<ProductDetailForSiteDto>()
                    {
                        Data = new ProductDetailForSiteDto
                        {
                            Brand = Product.Brand,
                            Category = $"{Product.Category.ParentCategory.Name} - {Product.Category.Name}",
                            Description = Product.Description,
                            Id = Product.Id,
                            Price = Product.Price,
                            Star = random.Next(1, 5),
                            Title = Product.Name,
                            Images = Product.ProductImages.Select(o => o.Src).ToList(),
                            Features = Product.ProductFeatures.Select(o => new ProductDetailForSite_FeaturesDto
                            {
                                DisplayName = o.DisplayName,
                                Value = o.Value,
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
                return new ResultDto<ProductDetailForSiteDto>()
                {
                    IsSuccess = false,
                    Message = str,
                };
                throw;
            }
        }

        #endregion
    }

    public class ProductDetailForSiteDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Star { get; set; }
        public List<string> Images { get; set; }
        public List<ProductDetailForSite_FeaturesDto> Features { get; set; }
    }

    public class ProductDetailForSite_FeaturesDto
    {
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
}
