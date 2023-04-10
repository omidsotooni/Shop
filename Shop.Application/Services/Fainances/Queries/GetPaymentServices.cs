using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Contexts;
using Shop.Common;
using Shop.Common.Dto;

namespace Shop.Application.Services.Fainances.Queries
{
    public class GetPaymentServices : IGetPaymentServices
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public GetPaymentServices(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public ResultDto<GetPaymentDto> GetPayment(Guid guid)
        {
            try
            {
                var payment = _context.Payments.Where(o => o.PaymentGuid == guid).FirstOrDefault();

                if (payment != null)
                {
                    return new ResultDto<GetPaymentDto>()
                    {
                        Data = new GetPaymentDto()
                        {
                            Amount = payment.Amount,
                            Id = payment.Id,
                        }
                    };
                }
                else
                {
                    return new ResultDto<GetPaymentDto>()
                    {
                        IsSuccess = false,
                        Message = "پرداختی پیدا نشد!",
                    };
                }
            }
            catch (Exception ex)
            {
                Utility.ExceptionMessage(ex);
                return new ResultDto<GetPaymentDto>()
                {
                    IsSuccess = false,
                };
            }
        }
        public ResultDto<PaymentsListForAdminDto> GetPaymentForAdmin(int Page = 1, int PageSize = 10)
        {
            try
            {
                int rowCount = 0;
                var payment = _context.Payments.Include(o => o.User).ToPaged(Page, PageSize, out rowCount)
                    .ToList().Select(x => new PaymentForAdminDto
                    {
                        Id = x.Id,
                        Amount = x.Amount,
                        Authority = x.Authority,
                        Guid = x.PaymentGuid,
                        IsPaid = x.IsPaid,
                        PaymentDate = x.PayDate,
                        RefId = x.RefId,
                        UserId = x.UserId,
                        UserName = x.User.FullName
                    }).ToList();
                return new ResultDto<PaymentsListForAdminDto>()
                {
                    Data = new PaymentsListForAdminDto()
                    {
                        PaymentForAdminDtos = payment,
                        CurrentPage = Page,
                        PageSize = PageSize,
                        RowCount = rowCount,
                    },
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                Utility.ExceptionMessage(ex);
                return new ResultDto<PaymentsListForAdminDto>()
                {
                    IsSuccess = false,
                };
            }
        }

        #endregion
    }
}
