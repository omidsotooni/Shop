namespace Shop.Application.Services.Fainances.Commands
{
    public class ResultRequestPaymentDto
    {
        public Guid PaymentGuid { get; set; }
        public int Amount { get; set; }
        public string Email { get; set; }
        public long RequestPaymentId { get; set; }
    }
}
