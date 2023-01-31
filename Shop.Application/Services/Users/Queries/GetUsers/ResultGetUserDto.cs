namespace Shop.Application.Services.Users.Queries.GetUsers
{
    public class ResultGetUserDto
    {
        public List<GetUsersDto> UsersDtos { get; set; }
        public int Rows { get; set; }
    }
}
