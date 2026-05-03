namespace EndPoint.Site.Models.ZarinpalModels
{
    public class PaymentResponseData
    {
        public int Code { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// A unique 32 characters length identifier of type `UUID` (Universal Unique Identifier) that Zarinpal
        /// Sent to client for each payment request. The Identifier always start with 'A' character.
        /// Sample: A 36 character lenght string, starting with A, like: A00000000000000000000000000217885159
        /// </summary>
        public string Authority { get; set; }
        public string FeeType { get; set; }
        public decimal Fee { get; set; }
    }
}
