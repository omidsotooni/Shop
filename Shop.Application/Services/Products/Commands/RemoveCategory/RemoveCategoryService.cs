using Shop.Application.Interfaces.Contexts;
using Shop.Common.Dto;

namespace Shop.Application.Services.Products.Commands.RemoveCategory
{
    public class RemoveCategoryService : IRemoveCategoryService
    {
        private readonly IDataBaseContext _context;
        public RemoveCategoryService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long CategoryId)
        {
            try
            {
                var category = _context.Categories.Find(CategoryId);
                if (category == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "دسته بندی مورد نظر پیدا نشد!"
                    };
                }
                category.RemoveTime = DateTime.Now;
                category.IsRemoved = true;
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "دسته بندی مورد نظر حذف شد!"
                };
            }
            catch (Exception ex)
            {
                string str = "Error From Server: ";
                if (!string.IsNullOrEmpty(ex.Message))
                    str += ex.Message;
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "دسته بندی مورد نظر پیدا نشد!"
                };
            }
        }
    }
}
