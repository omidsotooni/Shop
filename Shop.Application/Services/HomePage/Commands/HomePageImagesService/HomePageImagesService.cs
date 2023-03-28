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
                    IsActive = request.IsActive,
                };
                _context.HomePageImages.Add(homePageImages);
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

        public ResultDto ChangeStatusHomePageImage(long HomePageImageId)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                var homePageImageId = _context.HomePageImages.Find(HomePageImageId);
                if(homePageImageId == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "تصویر پیدا نشد"
                    };
                }
                homePageImageId.IsActive = !homePageImageId.IsActive;
                 homePageImageId.UpdateTime = DateTime.Now;
                _context.SaveChanges();
                transaction.Commit();

                string userstate = homePageImageId.IsActive == true ? "فعال" : "غیر فعال";
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = $"تصویر {userstate} شد!",
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                string str = "Error From Server: ";
                if (!string.IsNullOrEmpty(ex.Message))
                    str += ex.Message;
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "تصویر پیدا نشد" + str,
                };
            }
        }

        public ResultDto DeleteHomePageImage(long HomePageImageId)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                var homePageImageId = _context.HomePageImages.Find(HomePageImageId);
                if (homePageImageId == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "تصویر پیدا نشد"
                    };
                }
                homePageImageId.IsRemoved = true;
                homePageImageId.RemoveTime = DateTime.Now;
                _context.SaveChanges();
                transaction.Commit();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "تصویر حذف شد!",
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                string str = "Error From Server: ";
                if (!string.IsNullOrEmpty(ex.Message))
                    str += ex.Message;
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "تصویر پیدا نشد" + str,
                };
            }
        }

        public ResultDto<RequestEditHomePageImagesDto> EditHomePageImage(RequestEditHomePageImagesDto HomePageImageForEdit)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                var hpi = _context.HomePageImages.Where(o => o.Id == HomePageImageForEdit.Id).FirstOrDefault();
                if (hpi == null)
                {
                    return new ResultDto<RequestEditHomePageImagesDto>()
                    {
                        IsSuccess = false,
                        Message = "تصویر مورد نظر پیدا نشد!",
                    };
                }
                if(HomePageImageForEdit.file != null)
                {
                    hpi.Src = Utility.UploadFile(HomePageImageForEdit.file, _environment).FileNameAddress;
                }
                hpi.AltName = HomePageImageForEdit.AltName;
                hpi.IsActive = HomePageImageForEdit.IsActive;
                hpi.Link = HomePageImageForEdit.Link;
                hpi.ImageLocation = HomePageImageForEdit.ImageLocation;
                hpi.UpdateTime = DateTime.Now;
                _context.SaveChanges();
                transaction.Commit();

                return new ResultDto<RequestEditHomePageImagesDto>()
                {
                    Message = "اطلاعات تصویر ویرایش شد.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                string str = "Error From Server: ";
                if (!string.IsNullOrEmpty(ex.Message))
                    str += ex.Message;
                return new ResultDto<RequestEditHomePageImagesDto>()
                {
                    IsSuccess = false,
                    Message = "خطا رخ داد" + str,
                };
            }
        }

        public ResultDto<RequestEditHomePageImagesDto> GetHomePageImageForEdit(long HomePageImageId)
        {
            try
            {
                var homePageImage = _context.HomePageImages.Where(o => o.Id == HomePageImageId).FirstOrDefault();
                if(homePageImage == null)
                {
                    return new ResultDto<RequestEditHomePageImagesDto>()
                    {
                        IsSuccess = false,
                        Message = "تصویر مورد نظر پیدا نشد!",
                    };
                }
                return new ResultDto<RequestEditHomePageImagesDto>()
                {
                    Data = new RequestEditHomePageImagesDto()
                    {
                        Id = homePageImage.Id,
                        AltName = homePageImage.AltName,
                        Src = homePageImage.Src,
                        IsActive = homePageImage.IsActive,
                        Link = homePageImage.Link,
                        ImageLocation = homePageImage.ImageLocation,
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
                return new ResultDto<RequestEditHomePageImagesDto>()
                {
                    IsSuccess = false,
                    Message = "خطا رخ داد" + str,
                };
            }
        }

        #endregion
    }
}
