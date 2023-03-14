using Shop.Common.Dto;

namespace Shop.Application.Services.Products.Commands.RemoveCategory
{
    public interface IRemoveCategoryService
    {
        public ResultDto Execute(long CategoryId);
    }
}
