using Newtonsoft.Json;

namespace EndPoint.Site.Models.IDPayModels
{
    public class RequestIDPay
    {
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("mail")]
        public string Mail { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("callback")]
        public string Callback { get; set; }
    }

}
