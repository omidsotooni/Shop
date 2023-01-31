namespace Shop.Application.Services.Users.Queries.GetUsers
{
    public interface IGetUsersService
    {
        ResultGetUserDto Execute(RequsetGetUserDto _requset);
    }
}
