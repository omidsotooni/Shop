using Shop.Application.Interfaces.Contexts;
using Shop.Common;
using Shop.Common.Dto;

namespace Shop.Application.Services.HomePage.Queries.GetHomePageImagesService
{
    public class GetHomePageImagesService : IGetHomePageImagesService
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public GetHomePageImagesService(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public ResultDto<GetHomePageImaesAdminDto> GetHomePageImagesForAdmin(int Page = 1, int PageSize = 10)
        {
            try
            {
                int rowCount = 0;
                var homePageImages = _context.HomePageImages.ToPaged(Page, PageSize, out rowCount)
                    .OrderByDescending(o => o.Id).Select(o => new GetHomePageImaesAdminListDto
                    {
                        Id = o.Id,
                        IsActive = o.IsActive,
                        AltName = o.AltName,
                        Link = o.Link,
                        Src = o.Src,
                    }).ToList();

                return new ResultDto<GetHomePageImaesAdminDto>()
                {
                    Data = new GetHomePageImaesAdminDto()
                    {
                        HomePageImaes = homePageImages,
                        CurrentPage = Page,
                        PageSize = PageSize,
                        RowCount = rowCount,
                    },
                    IsSuccess = true,
                    Message = "",
                };
            }
            catch (Exception ex)
            {
                string str = "Error From Server: ";
                if (!string.IsNullOrEmpty(ex.Message))
                    str += ex.Message;
                return new ResultDto<GetHomePageImaesAdminDto>()
                {
                    IsSuccess = false,
                    Message = "خطا رخ داد!" + str,
                };
            }
        }
        #endregion
    }
}
