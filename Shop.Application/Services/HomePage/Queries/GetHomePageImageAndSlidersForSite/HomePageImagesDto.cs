using Shop.Domain.Entities.HomePages;

namespace Shop.Application.Services.HomePage.Queries.GetHomePageImageAndSlidersForSite
{
    public class HomePageImagesDto
    {
        public long Id { get; set; }
        public string Src { get; set; }
        public string Link { get; set; }
        public string AltName { get; set; }
        public bool IsActive { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }
}
