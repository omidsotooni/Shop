namespace EndPoint.Site.Models.ZarinpalModels
{
    public class VerifyResponseData
    {
        /// <summary>
        /// The integer value that indicates the State <br/>
        /// <b>100</b>: for successful operation <br/>
        /// <b>101</b>: for repeated yet previously succeeded operation <br/>
        /// <b>-X</b>: <a href="https://docs.zarinpal.com/paymentGateway/error.html">See error codes in here</a>
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// The reference number of succeeded payment
        /// </summary>
        public long RefId { get; set; }
        /// <summary>
        /// Masked Credit Card Number, Like: `1234-****-****-6789`
        /// </summary>
        public string CardPan { get; set; }
        /// <summary>
        /// Credit Card Hash Number - Type: SHA256 in case of later needs
        /// </summary>
        public string CardHash { get; set; }
        /// <summary>
        /// The person who pays for the commissions of the Zarinpal, Either the Payer (the one who pays) or the Payee (the one to whom money is paid)
        /// </summary>
        public string FeeType { get; set; }
        /// <summary>
        /// The amount of Zarinpal commissions
        /// </summary>
        public decimal Fee { get; set; }
    }
}
