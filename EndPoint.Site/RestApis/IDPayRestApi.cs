using EndPoint.Site.Models.IDPayModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shop.Common;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace EndPoint.Site.RestApis
{
    public class IDPayRestApi
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        private static readonly string BaseApiUrlIDPay = "https://" + ConstString.RequestUrlForIDPay;
        private static readonly string BaseWebUrlIDPay = "https://" + ConstString.GatewayUrlIDPay;
        private static readonly string SandboxStr = ConstString.IsSandboxIDPay.ToLower();
        public static async Task<PaymentResponseIDPay> PaymentIDPay(RequestIDPay requestIDPay, string MerchantID)
        {
            var sandbox = SandboxStr == "true"? "1" : "0";
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient.DefaultRequestHeaders.Add("X-API-KEY", MerchantID);
            HttpClient.DefaultRequestHeaders.Add("X-SANDBOX", sandbox);
            
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{BaseApiUrlIDPay}/payment"),
                Method = HttpMethod.Post
            };
            request.Content = new StringContent(JsonConvert.SerializeObject(requestIDPay),
                    Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await HttpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.BadGateway)
                throw new IDPayException(response.StatusCode, "Cannot contact IDPay Server");
            if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)
                throw new IDPayException(response.StatusCode,
                    "Cannot process the request due to bad request error.");
            if ((int)response.StatusCode >= 500)
                throw new IDPayException(response.StatusCode, "IDPay responded with an unknown error");

            var result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<PaymentResponseIDPay>(result, GetSerializerSetting());
        }
        public static async Task<PaymentInfoIDPay> VerifyIDPay(ResultPaymentIDPay resultPaymentIDPay, string MerchantID)
        {
            var sandbox = SandboxStr == "true" ? "1" : "0";
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient.DefaultRequestHeaders.Add("X-API-KEY", MerchantID);
            HttpClient.DefaultRequestHeaders.Add("X-SANDBOX", sandbox);
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{BaseWebUrlIDPay}"),
                Method = HttpMethod.Post
            };
            request.Content = new StringContent(JsonConvert.SerializeObject(resultPaymentIDPay),
                    Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await HttpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.BadGateway)
                throw new IDPayException(response.StatusCode, "Cannot contact IDPay Server");
            if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)
                throw new IDPayException(response.StatusCode,
                    "Cannot process the request due to bad request error.");
            if ((int)response.StatusCode >= 500)
                throw new IDPayException(response.StatusCode, "IDPay responded with an unknown error");

            var result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<PaymentInfoIDPay>(result, GetSerializerSetting());
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
