using Shop.Common.Dto;

namespace Shop.Application.Services.Common.Queries.GetCategory
{
    public interface IGetCategoryService
    {
        ResultDto<List<CategoryDto>> Execute();
    }
}
