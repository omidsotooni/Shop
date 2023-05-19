using Shop.Common.Dto;

namespace Shop.Application.Services.Users.Commands.RegisterUser
{
    public interface IRegisterUserService
    {
        ResultDto<ResultRegisterUserDto> Execute(RequsetRegisterUserDto _requset);
        void SendWelcomeMail(int RandomCode, string UserEmail);
    }
}
