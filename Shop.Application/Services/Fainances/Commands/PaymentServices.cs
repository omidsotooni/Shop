using Shop.Application.Interfaces.Contexts;
using Shop.Common;
using Shop.Common.Dto;
using Shop.Domain.Entities.Finances;

namespace Shop.Application.Services.Fainances.Commands
{
    public class PaymentServices : IPaymentServices
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public PaymentServices(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public ResultDto<ResultRequestPaymentDto> AddRequestPayment(int Amount, long UserId)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                var user = _context.Users.Find(UserId);
                if (user != null)
                {
                    Payment payment = new Payment()
                    {
                        Amount = Amount,
                        PaymentGuid = Guid.NewGuid(),
                        IsPaid = false,
                        User = user,
                        Authority = "null",
                        RefId = 30000,
                    };
                    _context.Payments.Add(payment);
                    _context.SaveChanges();
                    transaction.Commit();
                    return new ResultDto<ResultRequestPaymentDto>()
                    {
                        Data = new ResultRequestPaymentDto
                        {
                            PaymentGuid = payment.PaymentGuid,
                            Amount = payment.Amount,
                            Email = user.Email,
                            RequestPaymentId = payment.Id,
                        },
                        IsSuccess = true,
                    };
                }
                return new ResultDto<ResultRequestPaymentDto>()
                {
                    IsSuccess = false,
                    Message = "کاربر پیدا نشد!",
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Utility.ExceptionMessage(ex);
                return new ResultDto<ResultRequestPaymentDto>()
                {
                    IsSuccess = false,
                };
            }
        }

        #endregion
    }
}
