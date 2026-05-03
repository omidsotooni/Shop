using Newtonsoft.Json;

namespace EndPoint.Site.Models.IDPayModels
{
    public class RequestResponsFailed
    {
        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }
    }

}
