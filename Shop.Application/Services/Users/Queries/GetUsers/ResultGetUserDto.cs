namespace Shop.Application.Services.Users.Queries.GetUsers
{
    public class ResultGetUserDto
    {
        public List<GetUsersDto> UsersDtos { get; set; }
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
