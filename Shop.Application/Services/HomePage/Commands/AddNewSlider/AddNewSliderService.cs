using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Shop.Application.Interfaces.Contexts;
using Shop.Common;
using Shop.Common.Dto;
using Shop.Domain.Entities.HomePages;

namespace Shop.Application.Services.HomePage.Commands.AddNewSlider
{
    public class AddNewSliderService : IAddNewSliderService
    {
        #region Fields
        private readonly IHostingEnvironment _environment;
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public AddNewSliderService(IDataBaseContext context, IHostingEnvironment environment)
        {
            _environment = environment;
            _context = context;
        }
        #endregion

        #region Methods
        public ResultDto Execute(RequestAddNewSliderDto requestAdd, IFormFile file)
        {
            try
            {
                var SrcFile = Utility.UploadFile(file , _environment);               
                requestAdd.Src = SrcFile.FileNameAddress;
                Slider slider = new Slider()
                {
                    Link = requestAdd.Link,
                    Src = requestAdd.Src,
                    AltName = requestAdd.AltName,
                    IsActive = requestAdd.IsActive,
                };
                _context.Sliders.Add(slider);
                _context.SaveChanges();

                return new ResultDto()
                {
                    Message = "تصویر جدید برای اسلایدر اضافه شد",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                string str = "Error From Server: ";
                if (!string.IsNullOrEmpty(ex.Message))
                    str += ex.Message;
                return new ResultDto()
                {
                    Message = str,
                    IsSuccess = false
                };
            }
        }        
        
        #endregion
    }
}
