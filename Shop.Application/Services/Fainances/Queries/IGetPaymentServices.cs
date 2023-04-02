using Shop.Common.Dto;

namespace Shop.Application.Services.Fainances.Queries
{
    public interface IGetPaymentServices
    {
        ResultDto<GetPaymentDto> GetPayment(Guid guid);
    }
}
