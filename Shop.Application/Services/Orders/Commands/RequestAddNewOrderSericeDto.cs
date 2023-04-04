using static Shop.Common.Utility;

namespace Shop.Application.Services.Orders.Commands
{
    public class RequestAddNewOrderSericeDto
    {
        public long CartId { get; set; }
        public long PaymentId { get; set; }
        public long UserId { get; set; }
        public string Authority { get; set; }
        public string Address { get; set; }
        public long RefId { get; set; } = 0;
        public bool Status { get; set; } = false;
        public Banking BankingGateway { get; set; }
    }
}
