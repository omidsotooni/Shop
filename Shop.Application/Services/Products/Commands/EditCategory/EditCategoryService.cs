using Shop.Application.Interfaces.Contexts;
using Shop.Common.Dto;
using Shop.Domain.Entities.Products;

namespace Shop.Application.Services.Products.Commands.EditCategory
{
    public class EditCategoryService : IEditCategoryService
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public EditCategoryService(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public ResultDto Execute(Category request)
        {
            try
            {
                var category = _context.Categories.Find(request.Id);
                if (category == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "دسته بندی مورد نظر پیدا نشد!"
                    };
                }
                category.Name = request.Name;
                if(request.ParentCategoryId != null)
                    category.ParentCategoryId = request.ParentCategoryId;
                category.UpdateTime = DateTime.Now;
                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "ویرایش دسته بندی انجام شد"
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
        #endregion
    }
}
