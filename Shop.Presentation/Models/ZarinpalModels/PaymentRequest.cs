using Newtonsoft.Json;

namespace EndPoint.Site.Models.ZarinpalModels
{
    public class PaymentRequest
    {
        /// <summary>
        /// <i>Mandatory</i> The code that the Zarinpal is provided specifically for this Merchant
        /// </summary>
        public string MerchantId { get; set; }

        /// <summary>
        /// <i>Mandatory</i> The amount of money that should be transfer during this transaction in IRR (Iranian Rials)
        /// </summary>
        [JsonConverter(typeof(DecimalAsStringWithoutFloatingPointConverter))]
        public decimal Amount { get; set; }

        /// <summary>
        /// <i>Mandatory</i> The URL that the bank should send user into after performing the transaction, either successful or unsuccessful
        /// </summary>
        public string CallbackUrl { get; set; }

        /// <summary>
        /// <i>Mandatory</i> 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// <i>Optional</i> Provides messaging information to ease user from typing them
        /// </summary>
        public PaymentRequestMetadata Metadata { get; set; }
    }
}
