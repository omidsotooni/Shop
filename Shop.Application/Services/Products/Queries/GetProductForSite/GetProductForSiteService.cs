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
        public ResultDto<ResultProductForSiteDto> Execute(int Page)
        {
            try
            {
                int totalRow = 0;
                var poducts = _context.Products.Where(o => o.Displayed).
                    Include(o => o.ProductImages).ToPaged(Page, 5, out totalRow);
                Random random = new Random();
                return new ResultDto<ResultProductForSiteDto>
                {
                    Data = new ResultProductForSiteDto
                    {
                        TotalRow = totalRow,
                        Products = poducts.Select(o => new ProductForSiteDto
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
