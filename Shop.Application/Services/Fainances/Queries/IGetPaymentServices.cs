using Shop.Common.Dto;

namespace Shop.Application.Services.Fainances.Queries
{
    public interface IGetPaymentServices
    {
        ResultDto<GetPaymentDto> GetPayment(Guid guid);
        ResultDto<PaymentsListForAdminDto> GetPaymentForAdmin(int Page = 1, int PageSize = 10);
    }
}
