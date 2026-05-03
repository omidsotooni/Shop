using Newtonsoft.Json;

namespace EndPoint.Site.Models.ZarinpalModels
{
    public class VerifyRequest
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
        /// Documentation: A unique 32 characters length identifier of type `UUID` (Universal Unique Identifier) that Zarinpal
        /// Sent to client for each payment request. The Identifier always start with 'A' character.
        /// Sample: A 36 character lenght string, starting with A, like: A00000000000000000000000000217885159
        /// </summary>
        public string Authority { get; set; }
    }
}
