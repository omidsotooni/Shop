using Shop.Common.Dto;
using Shop.Domain.Entities.Products;

namespace Shop.Application.Services.Products.Queries.GetCategories
{
    public interface IGetCategoriesService
    {
        ResultDto<List<CategoriesDto>> Execute(long? ParentId);
        ResultDto<Category> GetCategoryById(long CategoryId);
        ResultDto<List<CategoriesDto>> GetCategories();
    }
}
