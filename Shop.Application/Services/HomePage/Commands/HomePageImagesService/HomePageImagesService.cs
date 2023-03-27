using Microsoft.AspNetCore.Hosting;
using Shop.Application.Interfaces.Contexts;
using Shop.Common;
using Shop.Common.Dto;
using Shop.Domain.Entities.HomePages;

namespace Shop.Application.Services.HomePage.Commands.HomePageImagesService
{
    public class HomePageImagesService : IHomePageImagesService
    {
        #region Fields
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        #endregion

        #region Constructor
        public HomePageImagesService(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        #endregion

        #region Methods
        public ResultDto AddHomePageImages(RequestHomePageImagesDto request)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                var SrcFile = Utility.UploadFile(request.file, _environment).FileNameAddress;
                if (SrcFile != null)
                {
                    request.Src = SrcFile;
                }
                HomePageImages homePageImages = new HomePageImages()
                {
                    Link = request.Link,
                    Src = request.Src,
                    AltName = request.AltName,                    
                    ImageLocation = request.ImageLocation,
                };
                _context.SaveChanges();
                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "تصویر جدید اضافه شد"
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                string str = "Error From Server: ";
                if (!string.IsNullOrEmpty(ex.Message))
                    str += ex.Message;
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا رخ داد" + str,
                };
            }
        }
        #endregion
    }
}
