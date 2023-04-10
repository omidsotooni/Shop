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
        private readonly HttpClient httpClient;
        private readonly string BaseApiUrlIDPay = "https://" + ConstString.RequestUrlForIDPay;
        private readonly string BaseWebUrlIDPay = "https://" + ConstString.GatewayUrlIDPay;
        private readonly string SandboxStr = ConstString.IsSandboxIDPay.ToLower();
        private readonly string MerchantIdIDPay = ConstString.MerchantIdIDPay;
        public IDPayRestApi()
        {
            var sandbox = SandboxStr == "true" ? "1" : "0";
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-API-KEY", MerchantIdIDPay);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("X-SANDBOX", sandbox);
        }

        //var body = @"{" + "\n" +
        //@"  ""order_id"": 101," + "\n" +
        //@"  ""amount"": 284800000," + "\n" +
        //@"  ""name"": ""پرداخت برای فروشگاه""," + "\n" +
        //@"  ""phone"": ""09011234567""," + "\n" +
        //@"  ""mail"": ""omidsotooni@shop.com""," + "\n" +
        //@"  ""desc"": ""پرداخت فاکتور شماره: 103""," + "\n" +
        //@"  ""callback"": ""https://localhost:7000/Payment/VerifyIDPay?PaymentGuid=a5136b88-3084-4361-bc6f-184b33c69225""" + "\n" +
        //@"}";
        //request.AddStringBody(body, DataFormat.Json);

        public async Task<PaymentRequestData> PaymentIDPay(RequestIDPay requestIDPay)
        {
            try
            {
                JsonSerializerSettings config = new()
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                PaymentRequestData result = new();
                string transactiosUrl = $"{BaseApiUrlIDPay}payment";
                var payload = JsonConvert.SerializeObject(requestIDPay, config);
                var content = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
                var httpResponse = await httpClient.PostAsync(transactiosUrl, content);

                result.Status = (int)httpResponse.StatusCode;
                if (httpResponse.StatusCode == HttpStatusCode.Created && httpResponse.Content is not null)
                {
                    string responseContent = await httpResponse.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<RequestResponsSuccess>(responseContent);
                    result.Data = data;
                }
                else if ((httpResponse.StatusCode is HttpStatusCode.Forbidden or HttpStatusCode.NotAcceptable
                    or HttpStatusCode.MethodNotAllowed) && httpResponse.Content is not null)
                {
                    string responseContent = await httpResponse.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<RequestResponsFailed>(responseContent);
                    result.Data = data;
                }
                return result;
            }
            catch (Exception ex)
            {
                Utility.ExceptionMessage(ex);
                return null;
            }
        }


        private HttpClient client = null;
        public async Task<object> payRestsharp(RequestIDPay requestIDPay, string MerchantID)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://api.idpay.ir/v1.1/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("X-API-KEY", "57b30e0d-b5b8-4291-adcd-0b1c6c94fd84");
            client.DefaultRequestHeaders.Add("X-SANDBOX", "1");

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(client.BaseAddress + "payment"),
                Method = HttpMethod.Post
            };
            request.Content = new StringContent(JsonConvert.SerializeObject(requestIDPay),
                Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string _data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<RequestResponsSuccess>(_data);
            }
            else
            {
                string _data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<RequestResponsFailed>(_data);
            }
        }

        public async void payclient()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.idpay.ir/v1.1/payment");
            request.Headers.Add("X-API-KEY", "57b30e0d-b5b8-4291-adcd-0b1c6c94fd84");
            request.Headers.Add("X-SANDBOX", "1");
            request.Headers.Add("Cookie", "SSESS39ff69be91203b0b4d2039dd7a713620=8x3SzC1e04_pxm4xy2rBV5eUYaUMkDzP_iGg3WnPtBE");
            var content = new StringContent("{\n  \"order_id\": 101,\n  \"amount\": 284800000,\n  \"name\": \"پرداخت برای فروشگاه\",\n  \"phone\": \"09011234567\",\n  \"mail\": \"omidsotooni@shop.com\",\n  \"desc\": \"پرداخت فاکتور شماره: 103\",\n  \"callback\": \"https://localhost:7000/Payment/VerifyIDPay?PaymentGuid=a5136b88-3084-4361-bc6f-184b33c69225\"\n}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

        }

        //public void verify()
        //{
        //    var options = new RestClientOptions("")
        //    {
        //        MaxTimeout = -1,
        //    };
        //    var client = new RestClient(options);
        //    var request = new RestRequest("https://api.idpay.ir/v1.1/payment/verify", Method.Post);
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddHeader("X-API-KEY", "57b30e0d-b5b8-4291-adcd-0b1c6c94fd84");
        //    request.AddHeader("X-SANDBOX", "1");
        //    request.AddHeader("Cookie", "SSESS39ff69be91203b0b4d2039dd7a713620=4zM9OR5dirkjzDcIrfxrC-pYosl4EiBmu2oBpxQ9ib0");
        //    var body = @"{" + "\n" +
        //    @"  ""id"": ""e0745a16eb267e75a07561951b11e53d""," + "\n" +
        //    @"  ""order_id"": ""101""" + "\n" +
        //    @"}";
        //    request.AddStringBody(body, DataFormat.Json);
        //    RestResponse response = await client.ExecuteAsync(request);
        //    Console.WriteLine(response.Content);
        //}

        //public void inqury()
        //{
        //    var options = new RestClientOptions("")
        //    {
        //        MaxTimeout = -1,
        //    };
        //    var client = new RestClient(options);
        //    var request = new RestRequest("https://api.idpay.ir/v1.1/payment/inquiry", Method.Post);
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddHeader("X-API-KEY", "57b30e0d-b5b8-4291-adcd-0b1c6c94fd84");
        //    request.AddHeader("X-SANDBOX", "1");
        //    request.AddHeader("Cookie", "SSESS39ff69be91203b0b4d2039dd7a713620=4zM9OR5dirkjzDcIrfxrC-pYosl4EiBmu2oBpxQ9ib0");
        //    var body = @"{" + "\n" +
        //    @"  ""id"": ""e0745a16eb267e75a07561951b11e53d""," + "\n" +
        //    @"  ""order_id"": ""101""" + "\n" +
        //    @"}";
        //    request.AddStringBody(body, DataFormat.Json);
        //    RestResponse response = await client.ExecuteAsync(request);
        //    Console.WriteLine(response.Content);
        //}

        public async Task<PaymentResponseIDPay> PaymentIDPay1(RequestIDPay requestIDPay, string MerchantID)
        {
            try
            {
                var sandbox = SandboxStr == "true" ? "1" : "0";
                httpClient.BaseAddress = new Uri(BaseApiUrlIDPay);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("X-API-KEY", MerchantID);
                httpClient.DefaultRequestHeaders.Add("X-SANDBOX", sandbox);

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri($"{BaseApiUrlIDPay}payment"),
                    Method = HttpMethod.Post
                };
                request.Content = new StringContent(JsonConvert.SerializeObject(requestIDPay),
                        Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await httpClient.SendAsync(request);
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
            catch (Exception ex)
            {
                Utility.ExceptionMessage(ex);
                return null;
            }
        }
        public async Task<PaymentInfoIDPay> VerifyIDPay(ResultPaymentIDPay resultPaymentIDPay, string MerchantID)
        {
            var sandbox = SandboxStr == "true" ? "1" : "0";
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("X-API-KEY", MerchantID);
            httpClient.DefaultRequestHeaders.Add("X-SANDBOX", sandbox);
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{BaseWebUrlIDPay}"),
                Method = HttpMethod.Post
            };
            request.Content = new StringContent(JsonConvert.SerializeObject(resultPaymentIDPay),
                    Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await httpClient.SendAsync(request);
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
