namespace Shop.Application.Services.Users.Commands.RegisterUser
{
    public class RequsetRegisterUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePasword { get; set; }
        public List<RolesInRegisterUserDto> Roles { get; set; }
    }
}
