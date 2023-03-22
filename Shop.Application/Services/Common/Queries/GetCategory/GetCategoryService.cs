using Shop.Application.Interfaces.Contexts;
using Shop.Common.Dto;

namespace Shop.Application.Services.Common.Queries.GetCategory
{
    public class GetCategoryService : IGetCategoryService
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public GetCategoryService(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public ResultDto<List<CategoryDto>> Execute()
        {
            try
            {
                var category = _context.Categories.Where(o => o.ParentCategoryId == null).ToList()
                    .Select(o => new CategoryDto
                    {
                        CategoryId = o.Id,
                        CategoryName = o.Name,
                    }).ToList();

                return category == null ? throw new Exception("Products Not Found !")
                    : new ResultDto<List<CategoryDto>>()
                    {
                        Data = category,
                        IsSuccess = true,
                    };
            }
            catch (Exception ex)
            {
                string str = "Error From Server: ";
                if (!string.IsNullOrEmpty(ex.Message))
                    str += ex.Message;
               return new ResultDto<List<CategoryDto>>()
                {
                    Message = str,
                    IsSuccess = false,
                };
            }
        }

        #endregion
    }
    public class CategoryDto
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

}
