using Shop.Common.Dto;

namespace Shop.Application.Services.Orders.Commands
{
    public interface IOrderServices
    {
        ResultDto AddNewOrder(RequestAddNewOrderSericeDto request);
    }
}
