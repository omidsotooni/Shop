using Shop.Application.Interfaces.Contexts;
using Shop.Common.Dto;

namespace Shop.Application.Services.HomePage.Commands.SliderSatusChangeService
{
    public class SliderSatusChangeService : ISliderSatusChangeService
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public SliderSatusChangeService(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public ResultDto ChangeStatusSlider(long SliderId)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                var slider = _context.Sliders.Find(SliderId);
                if(slider == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "اسلایدر پیدا نشد"
                    };
                }
                slider.IsActive = !slider.IsActive;
                slider.UpdateTime = DateTime.Now;
                _context.SaveChanges();
                transaction.Commit();

                string userstate = slider.IsActive == true ? "فعال" : "غیر فعال";
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = $"اسلایدر {userstate} شد!",
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
                    Message = "اسلایدر پیدا نشد" + str,
                };
            }
        }

        #endregion
    }
}
