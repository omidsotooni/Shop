using ShopWithASP.NETCore.Application.Interfaces.Contexts;
using ShopWithASP.NETCore.Common;

namespace ShopWithASP.NETCore.Application.Services.Users.Queries.GetUsers
{
    public class GetUsersService : IGetUsersService
    {
        private readonly IDataBaseContext _context;
        public GetUsersService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultGetUserDto Execute(RequsetGetUserDto _requset)
        {
            var _users = _context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(_requset.SearchKey))
            {
                _users = _users.Where(o => o.FullName.Contains(_requset.SearchKey) || o.Email.Contains(_requset.SearchKey));
            }
            int RowsCount = 0;
            var _userlist = _users.ToPaged(_requset.Page, 20, out RowsCount).Select(o => new GetUsersDto
            {
                Email = o.Email,
                FullName = o.FullName,
                UserId = o.Id,
                IsActive = o.IsActive,
            }).ToList();
            
            return new ResultGetUserDto
            {
                Rows = RowsCount,
                UsersDtos = _userlist,
            };
        }
    }
}
