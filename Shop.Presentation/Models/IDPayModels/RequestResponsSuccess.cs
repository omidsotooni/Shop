using Newtonsoft.Json;

namespace EndPoint.Site.Models.IDPayModels
{
    public class RequestResponsSuccess
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }
    }

}
