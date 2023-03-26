using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Shop.Application.Interfaces.Contexts;
using Shop.Application.Services.Products.Commands.AddNewProduct;
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
                var SrcFile = UploadFile(file);               
                requestAdd.Src = SrcFile.FileNameAddress;
                Slider slider = new Slider()
                {
                    Link = requestAdd.Link,
                    Src = requestAdd.Src,
                    AltName = requestAdd.AltName,                    
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
        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\HomePages\Slider\";
                var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }
                if (file == null || file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }
                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return new UploadDto()
                {
                    FileNameAddress = folder + fileName,
                    Status = true,
                };
            }
            return null;
        }
        #endregion
    }
}
