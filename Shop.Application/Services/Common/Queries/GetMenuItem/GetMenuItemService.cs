using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Contexts;
using Shop.Common.Dto;

namespace Shop.Application.Services.Common.Queries.GetMenuItem
{
    public class GetMenuItemService : IGetMenuItemService
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public GetMenuItemService(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public ResultDto<List<MenuItemDto>> Execute()
        {
            try
            {
                var category = _context.Categories.Include(o => o.SubCategories)
               .Where(o => o.ParentCategoryId == null).ToList()
               .Select(o => new MenuItemDto
               {
                   CategoryId = o.Id,
                   Name = o.Name,
                   Child = o.SubCategories.ToList().Select(child => new MenuItemDto
                   {
                       CategoryId = child.Id,
                       Name = child.Name,
                   }).ToList(),
               }).ToList();

                return category == null ? throw new Exception("Categories Not Found !")
                    : new ResultDto<List<MenuItemDto>>()
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
                return new ResultDto<List<MenuItemDto>>()
                {
                    IsSuccess = true,
                    Message = str,
                };
            }
        }

        #endregion
    }
    public class MenuItemDto
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public List<MenuItemDto> Child { get; set; }
    }
}
