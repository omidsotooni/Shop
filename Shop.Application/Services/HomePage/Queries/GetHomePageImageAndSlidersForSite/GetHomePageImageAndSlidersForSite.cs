using Shop.Application.Interfaces.Contexts;
using Shop.Common.Dto;

namespace Shop.Application.Services.HomePage.Queries.GetHomePageImageAndSlidersForSite
{
    public class GetHomePageImageAndSlidersForSite : IGetHomePageImageAndSlidersForSite
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public GetHomePageImageAndSlidersForSite(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public ResultDto<List<HomePageImagesDto>> GetHomePageImages()
        {
            try
            {
                var images = _context.HomePageImages.Where(o => o.IsActive).OrderByDescending(o => o.Id)
                .Select(o => new HomePageImagesDto
                {
                    Id = o.Id,
                    Src = o.Src,
                    Link = o.Link,
                    AltName = o.AltName,
                    IsActive = o.IsActive,
                    ImageLocation = o.ImageLocation,
                }).ToList();
                return new ResultDto<List<HomePageImagesDto>>()
                {
                    Data = images,
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                string str = "Error From Server: ";
                if (!string.IsNullOrEmpty(ex.Message))
                    str += ex.Message;
                return new ResultDto<List<HomePageImagesDto>>()
                {
                    IsSuccess = false,
                };
            }
        }

        public ResultDto<List<SlidersDto>> GetHomePageSliders()
        {
            try
            {
                var images = _context.Sliders.Where(o => o.IsActive).OrderByDescending(o => o.Id)
                .Select(o => new SlidersDto
                {
                    Src = o.Src,
                    Link = o.Link,
                    AltName = o.AltName,
                    IsActive = o.IsActive,
                }).ToList();
                return new ResultDto<List<SlidersDto>>()
                {
                    Data = images,
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                string str = "Error From Server: ";
                if (!string.IsNullOrEmpty(ex.Message))
                    str += ex.Message;
                return new ResultDto<List<SlidersDto>>()
                {
                    IsSuccess = false,
                };
            }
        }
        #endregion
    }
}
