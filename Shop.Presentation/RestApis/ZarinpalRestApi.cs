using EndPoint.Site.Models.ZarinpalModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shop.Common;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace EndPoint.Site.RestApis
{
    public class ZarinpalRestApi
    {
        private static readonly HttpClient HttpClient = new HttpClient();        
        private static readonly string BaseApiUrl = "https://" + ConstString.RequestUrlForZarinpal;
        private static readonly string BaseWebUrl = "https://" + ConstString.GatewayUrlZarinpal;
        public static PaymentResponse Payment(PaymentRequest request)
        {
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var serializeObject = JsonConvert.SerializeObject(request, GetSerializerSetting());

            var stringContent = new StringContent(serializeObject, Encoding.UTF8, "application/json");

            var httpResponseMessage =
                HttpClient.PostAsync($"{BaseApiUrl}/pg/v4/payment/request.json", stringContent).Result;

            if (httpResponseMessage.StatusCode == HttpStatusCode.BadGateway)
                throw new ZarinpalException(httpResponseMessage.StatusCode, "Cannot contact Zarinpal Server");
            if ((int)httpResponseMessage.StatusCode >= 400 && (int)httpResponseMessage.StatusCode < 500)
                throw new ZarinpalException(httpResponseMessage.StatusCode,
                    "Cannot process the request due to bad request error.");
            if ((int)httpResponseMessage.StatusCode >= 500)
                throw new ZarinpalException(httpResponseMessage.StatusCode, "Zarinpal responded with an unknown error");


            var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<PaymentResponse>(result, GetSerializerSetting());
        }

        public static string GenerateGatewayLink(string authority)
        {
            return $"{BaseWebUrl}/{authority}";
        }

        public static VerifyResponse Verify(VerifyRequest request)
        {
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var serializeObject = JsonConvert.SerializeObject(request, GetSerializerSetting());

            var stringContent = new StringContent(serializeObject, Encoding.UTF8, "application/json");

            var httpResponseMessage =
                HttpClient.PostAsync($"{BaseApiUrl}/pg/v4/payment/verify.json", stringContent).Result;

            if (httpResponseMessage.StatusCode == HttpStatusCode.BadGateway)
                throw new ZarinpalException(httpResponseMessage.StatusCode, "Cannot contact Zarinpal Server");
            if ((int)httpResponseMessage.StatusCode >= 400 && (int)httpResponseMessage.StatusCode < 500)
                throw new ZarinpalException(httpResponseMessage.StatusCode,
                    "Cannot process the request due to bad request error.");
            if ((int)httpResponseMessage.StatusCode >= 500)
                throw new ZarinpalException(httpResponseMessage.StatusCode, "Zarinpal responded with an unknown error");

            var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<VerifyResponse>(result, GetSerializerSetting());
        }
        private static JsonSerializerSettings GetSerializerSetting()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                NullValueHandling = NullValueHandling.Ignore
            };
            return jsonSerializerSettings;
        }
    }
}
