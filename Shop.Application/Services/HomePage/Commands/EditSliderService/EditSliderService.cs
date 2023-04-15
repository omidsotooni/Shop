using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Shop.Application.Interfaces.Contexts;
using Shop.Application.Services.Products.Commands.AddNewProduct;
using Shop.Common;
using Shop.Common.Dto;

namespace Shop.Application.Services.HomePage.Commands.EditSliderService
{
    public class EditSliderService : IEditSliderService
    {
        #region Fields
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        #endregion

        #region Constructor
        public EditSliderService(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }
        #endregion

        #region Methods
        public ResultDto<EditSliderDto> EditSlider(EditSliderDto Slider, IFormFile file)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                string ImageFor = "HomePages";
                var s = _context.Sliders.Find(Slider.Id);
                if (s == null)
                {
                    return new ResultDto<EditSliderDto>()
                    {
                        IsSuccess = false,
                        Message = "اسلایدر مورد نظر پیدا نشد!"
                    };
                }
                if(file != null)
                {
                    s.Src = Utility.UploadFile(file, _environment, ImageFor).FileNameAddress;
                }
                s.AltName = Slider.AltName;
                s.IsActive = Slider.IsActive;
                s.Link = Slider.Link;
                s.UpdateTime = DateTime.Now;
                _context.SaveChanges();
                transaction.Commit();

                return new ResultDto<EditSliderDto>()
                {
                    Message = "اطلاعات اسلایدر ویرایش شد.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                string str = "Error From Server: ";
                if (!string.IsNullOrEmpty(ex.Message))
                    str += ex.Message;
                return new ResultDto<EditSliderDto>()
                {
                    IsSuccess = false,
                    Message = "اسلایدر مورد نظر پیدا نشد!" + str,
                };
            }
        }

        public ResultDto<EditSliderDto> GetSlider(long sliderId)
        {
            try
            {
                var slider = _context.Sliders.Where(o => o.Id == sliderId).FirstOrDefault();
                if(slider == null)
                {
                    return new ResultDto<EditSliderDto>()
                    {
                        IsSuccess = false,
                        Message = "اسلایدر مورد نظر پیدا نشد!"
                    };
                }
                return new ResultDto<EditSliderDto>()
                {
                    Data = new EditSliderDto()
                    {
                        Id = slider.Id,
                        AltName = slider.AltName,
                        Src = slider.Src,
                        IsActive = slider.IsActive,
                        Link = slider.Link,
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
                return new ResultDto<EditSliderDto>()
                {
                    IsSuccess = false,
                    Message = "اسلایدر مورد نظر پیدا نشد!" + str,
                };
            }
        }
        
        #endregion
    }

}
