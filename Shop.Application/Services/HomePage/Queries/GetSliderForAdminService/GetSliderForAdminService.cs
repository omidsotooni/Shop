using Shop.Application.Interfaces.Contexts;
using Shop.Common;
using Shop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services.HomePage.Queries.GetSliderForAdminService
{
    public class GetSliderForAdminService : IGetSliderForAdminService
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public GetSliderForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public ResultDto<SliderForAdminDto> GetSlidersForAdmin(int Page = 1, int PageSize = 10)
        {
            try
            {
                int rowCount = 0;
                var sliders = _context.Sliders.ToPaged(Page, PageSize, out rowCount)
                    .OrderByDescending(x => x.Id).Select(o => new SliderForAdminListDto
                    {
                        Id = o.Id,
                        IsActive = o.IsActive,
                        AltName = o.AltName,
                        Link = o.Link,
                        Src = o.Src,
                    }).ToList();
                return new ResultDto<SliderForAdminDto>()
                {
                    Data = new SliderForAdminDto()
                    {
                        Sliders = sliders,
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
                return new ResultDto<SliderForAdminDto>()
                {
                    IsSuccess = false,
                    Message = str,
                };
            }
        }

        #endregion
    }
}