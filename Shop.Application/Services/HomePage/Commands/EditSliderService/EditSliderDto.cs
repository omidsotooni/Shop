namespace Shop.Application.Services.HomePage.Commands.EditSliderService
{
    public class EditSliderDto
    {
        public long Id { get; set; }
        public string Src { get; set; }
        public string Link { get; set; }
        public string AltName { get; set; }
        public bool IsActive { get; set; }
    }

}
