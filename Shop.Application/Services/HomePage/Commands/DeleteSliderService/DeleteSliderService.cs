using Shop.Application.Interfaces.Contexts;
using Shop.Common.Dto;

namespace Shop.Application.Services.HomePage.Commands.DeleteSliderService
{
    public class DeleteSliderService : IDeleteSliderService
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public DeleteSliderService(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public ResultDto DeleteSlider(long SliderId)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                var slider = _context.Sliders.Find(SliderId);
                if (slider == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "اسلایدر پیدا نشد"
                    };
                }
                slider.IsRemoved = true;
                slider.RemoveTime = DateTime.Now;
                _context.SaveChanges();
                transaction.Commit();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = $"اسلایدر حذف شد!",
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
