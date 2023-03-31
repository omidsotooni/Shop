using Shop.Application.Interfaces.Contexts;
using Shop.Common;
using Shop.Common.Dto;

namespace Shop.Application.Services.Fainances.Queries
{
    public class GetPaymentServices: IGetPaymentServices
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
        #endregion
    }
}
