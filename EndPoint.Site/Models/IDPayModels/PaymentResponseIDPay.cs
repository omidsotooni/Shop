namespace EndPoint.Site.Models.IDPayModels
{
    public class PaymentResponseIDPay
    {
        public string id { get; set; }
        public string link { get; set; }
        public int error_code { get; set; }
        public string error_message { get; set; }        
    }
}
