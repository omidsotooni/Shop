using Shop.Application.Interfaces.Contexts;
using Shop.Common;
using Shop.Common.Dto;
using Microsoft.EntityFrameworkCore;

namespace Shop.Application.Services.Users.Commands.UserLogin
{
    public class UserLoginService : IUserLoginService
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public UserLoginService(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public ResultDto<ResultUserloginDto> Execute(string Username, string Password)
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                return new ResultDto<ResultUserloginDto>()
                {
                    Data = new ResultUserloginDto()
                    {
                    },
                    IsSuccess = false,
                    Message = "نام کاربری و رمز عبور را وارد نمایید",
                };
            }
            var user = _context.Users.Include(p => p.UserInRoles).ThenInclude(p => p.Role)
                .Where(p => p.Email.Equals(Username) && p.IsActive == true).FirstOrDefault();

            if (user == null)
            {
                return new ResultDto<ResultUserloginDto>()
                {
                    Data = new ResultUserloginDto()
                    {

                    },
                    IsSuccess = false,
                    Message = "کاربری با این ایمیل در سایت فروشگاه ثبت نام نکرده است",
                };
            }
            var passwordHasher = new HashPassword();
            bool resultVerifyPassword = passwordHasher.VerifyPassword(user.Password, Password);
            if (resultVerifyPassword == false)
            {
                return new ResultDto<ResultUserloginDto>()
                {
                    Data = new ResultUserloginDto()
                    {
                    },
                    IsSuccess = false,
                    Message = "رمز وارد شده اشتباه است!",
                };
            }
            List<string> roles = new List<string>();
            foreach (var item in user.UserInRoles)
            {
                roles.Add(item.Role.Name);
            }

            return new ResultDto<ResultUserloginDto>()
            {
                Data = new ResultUserloginDto()
                {
                    Roles = roles,
                    UserId = user.Id,
                    Name = user.FullName
                },
                IsSuccess = true,
                Message = "ورود به سایت با موفقیت انجام شد",
            };
        }

        #endregion
    }
}
