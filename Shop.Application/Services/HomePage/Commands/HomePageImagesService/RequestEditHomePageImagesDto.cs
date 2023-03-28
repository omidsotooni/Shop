using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities.HomePages;

namespace Shop.Application.Services.HomePage.Commands.HomePageImagesService
{
    public class RequestEditHomePageImagesDto
    {
        public long Id { get; set; }
        public string Src { get; set; }
        public string Link { get; set; }
        public string AltName { get; set; }
        public bool IsActive { get; set; }
        public IFormFile file { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }
}
