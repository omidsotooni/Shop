using Shop.Common.Dto;
using Shop.Domain.Entities.Products;

namespace Shop.Application.Services.Products.Commands.EditCategory
{
    public interface IEditCategoryService
    {
        ResultDto Execute(Category request);
    }
}
