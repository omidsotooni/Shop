namespace EndPoint.Site.Models.IDPayModels
{
    public class PaymentResponseIDPay
    {
        public string id { get; set; }
        public string link { get; set; }
        public int error_code { get; set; }
        public string error_message { get; set; }
    }
    public class PaymentRequestData
    {
        public int Status { get; set; }
        public dynamic? Data { get; set; }
    }
}
