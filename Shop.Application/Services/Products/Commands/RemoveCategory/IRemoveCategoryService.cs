using Shop.Common.Dto;

namespace Shop.Application.Services.Products.Commands.RemoveCategory
{
    public interface IRemoveCategoryService
    {
        ResultDto Execute(long CategoryId);
    }
}
