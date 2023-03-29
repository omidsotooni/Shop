using Shop.Common.Dto;

namespace Shop.Application.Services.Common.Queries.GetMenuItem
{
    public interface IGetMenuItemService
    {
        ResultDto<List<MenuItemDto>> Execute();
        ResultDto<List<MenuItemForMobileDto>> GetMenuItemForMobile();
    }
}
