using Shop.Common.Dto;

namespace Shop.Application.Services.Fainances.Commands
{
    public interface IPaymentServices
    {
        ResultDto<ResultRequestPaymentDto> AddRequestPayment(int Amount, long UserId);
    }
}
