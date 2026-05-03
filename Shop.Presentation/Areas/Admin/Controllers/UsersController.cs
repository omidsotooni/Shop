using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Application.Services.Users.Commands.EditUser;
using Shop.Application.Services.Users.Commands.RegisterUser;
using Shop.Application.Services.Users.Commands.RemoveUser;
using Shop.Application.Services.Users.Commands.UserSatusChange;
using Shop.Application.Services.Users.Queries.GetRoles;
using Shop.Application.Services.Users.Queries.GetUsers;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IGetUsersService _getUsersService;
        private readonly IGetRolesService _getRolesService;
        private readonly IRegisterUserService _registerUserService;
        private readonly IRemoveUserService _removeUserService;
        private readonly IUserSatusChangeService _userSatusChangeService;
        private readonly IEditUserService _editUserService;
        public UsersController(IGetUsersService getUsersService, IGetRolesService getRolesService,
            IRegisterUserService registerUserService, IRemoveUserService removeUserService,
            IUserSatusChangeService userSatusChangeService, IEditUserService editUserService)
        {
            _getUsersService = getUsersService;
            _getRolesService = getRolesService;
            _registerUserService = registerUserService;
            _removeUserService = removeUserService;
            _userSatusChangeService = userSatusChangeService;
            _editUserService = editUserService;
        }

        public IActionResult Index(string SearchKey, int Page = 1, int PageSize = 20)
        {
            return View(_getUsersService.Execute(new RequsetGetUserDto
            {
                Page = Page,
                SearchKey = SearchKey,
                PageSize = PageSize,
            }));
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_getRolesService.Execute().Data, "RoleId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(string FullName, string Email, long RoleId, string Password, string RePassword)
        {
            var _result = _registerUserService.Execute(new RequsetRegisterUserDto
            {
                FullName = FullName,
                Email = Email,
                Roles = new List<RolesInRegisterUserDto>()
                {
                    new RolesInRegisterUserDto
                    {
                        RoleId = RoleId
                    }
                },
                Password = Password,
                RePasword = RePassword
            });
            return Json(_result);
        }

        [HttpPost]
        public IActionResult Delete(long UserId)
        {
            return Json(_removeUserService.Execute(UserId));
        }

        [HttpPost]
        public IActionResult UserSatusChange(long UserId)
        {
            return Json(_userSatusChangeService.Execute(UserId));
        }

        [HttpPost]
        public IActionResult Edit(long UserId, string Fullname)
        {
            return Json(_editUserService.Execute(new RequestEdituserDto
            {
                Fullname = Fullname,
                UserId = UserId,
            }));
        }

    }
}
