using Shop.Common;

namespace EndPoint.Site.Models.IDPayModels
{
    public class PaymentInfoIDPay
    {
        public int error_code { get; set; }
        public string error_message { get; set; }
        public int status { get; set; }
        public string track_id { get; set; }
        public string id { get; set; }
        public string order_id { get; set; }
        public decimal amount { get; set; }
        public double date { get; set; }
        public PaymentDetail payment { get; set; }
        public VerifyDate verify { get; set; }

        public DateTime Date
        {
            get
            {
                return Utility.UnixTimeStampToDateTime(date);
            }
        }
        public bool IsOK { get; set; }
        public string Message
        {
            get
            {
                return Utility.GetStatusIDPay(status);
            }
        }
    }

}
