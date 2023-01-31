using Shop.Common.Dto;

namespace Shop.Application.Services.Users.Commands.UserSatusChange
{

    public interface IUserSatusChangeService
    {
        ResultDto Execute(long UserId);
    }
}
