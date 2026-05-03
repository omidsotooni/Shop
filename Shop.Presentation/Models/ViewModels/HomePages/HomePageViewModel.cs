using Shop.Application.Services.HomePage.Queries.GetHomePageImageAndSlidersForSite;
using Shop.Application.Services.Products.Queries.GetProductForSite;

namespace EndPoint.Site.Models.ViewModels.HomePages
{
    public class HomePageViewModel
    {
        public List<SlidersDto> Sliders { get; set; }
        public List<HomePageImagesDto> HomePageImagesDtos { get; set; }
        public List<ProductForSiteDto> CameraCategory { get; set; }
        public List<ProductForSiteDto> MobileCategory { get; set; }
        public List<ProductForSiteDto> HomeAppliancesCategory { get; set; }
        public List<ProductForSiteDto> LaptopAndComputerCategory { get; set; }
    }
}
