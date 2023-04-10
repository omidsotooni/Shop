namespace Shop.Application.Services.Fainances.Queries
{
    public class GetPaymentDto
    {
        public int Amount { get; set; }
        public long Id { get; set; }
    }
    public class PaymentForAdminDto
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string UserName { get; set; }
        public long UserId { get; set; }
        public int Amount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Authority { get; set; }
        public long RefId { get; set; } = 0;
    }
    public class PaymentsListForAdminDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<PaymentForAdminDto> PaymentForAdminDtos { get; set; }
    }
}
