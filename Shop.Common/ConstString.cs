using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common
{
    public class ConstString
    {
        /// <summary>
        /// Read String by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string ReadConfig(string key)
        {
            Dictionary<string, string> configuration = new Dictionary<string, string>();
            string[] configArray = File.ReadAllLines("ConstStr.txt");
            foreach (string config in configArray)
            {
                string[] temp = config.Split(":");
                configuration.Add(temp[0].Trim(), temp[1].Trim());
            }
            return configuration[key];
        }

        public static string PortCallBackurl
        {
            get
            {
                try
                {
                    return ReadConfig("PortCallBackurl");
                }
                catch (Exception)
                {
                    return "data not found!";
                }
            }
        }
        public static string ServerCallBackurl
        {
            get
            {
                try
                {
                    return ReadConfig("ServerCallBackurl");
                }
                catch (Exception)
                {
                    return "data not found!";
                }
            }
        }
        public static string CallBackurlZarinpal
        {
            get
            {
                try
                {
                    return ReadConfig("CallBackurlZarinpal");
                }
                catch (Exception)
                {
                    return "data not found!";
                }
            }
        }
        public static string HttpCallBackurl
        {
            get
            {
                try
                {
                    return ReadConfig("HttpCallBackurl");
                }
                catch (Exception)
                {
                    return "data not found!";
                }
            }
        }
        public static string MerchantIdZarinpal
        {
            get
            {
                try
                {
                    return ReadConfig("MerchantIdZarinpal");
                }
                catch (Exception)
                {
                    return "data not found!";
                }
            }
        }
        public static string RequestUrlForZarinpal
        {
            get
            {
                try
                {
                    return ReadConfig("RequestUrlForZarinpal");
                }
                catch (Exception)
                {
                    return "data not found!";
                }
            }
        }
        public static string GatewayUrlZarinpal
        {
            get
            {
                try
                {
                    return ReadConfig("GatewayUrlZarinpal");
                }
                catch (Exception)
                {
                    return "data not found!";
                }
            }
        }
        public static string MerchantIdIDPay
        {
            get
            {
                try
                {
                    return ReadConfig("MerchantIdIDPay");
                }
                catch (Exception)
                {
                    return "data not found!";
                }
            }
        }
        public static string RequestUrlForIDPay
        {
            get
            {
                try
                {
                    return ReadConfig("RequestUrlForIDPay");
                }
                catch (Exception)
                {
                    return "data not found!";
                }
            }
        }
        public static string GatewayUrlIDPay
        {
            get
            {
                try
                {
                    return ReadConfig("GatewayUrlIDPay");
                }
                catch (Exception)
                {
                    return "data not found!";
                }
            }
        }
        public static string IsSandboxIDPay
        {
            get
            {
                try
                {
                    return ReadConfig("IsSandboxIDPay");
                }
                catch (Exception)
                {
                    return "data not found!";
                }
            }
        }
        public static string CallBackurlIDPay
        {
            get
            {
                try
                {
                    return ReadConfig("CallBackurlIDPay");
                }
                catch (Exception)
                {
                    return "data not found!";
                }
            }
        }
        public static long CameraSliderForSiteId
        {
            get
            {
                try
                {
                    return long.Parse(ReadConfig("CameraSliderForSiteId"));
                }
                catch (Exception)
                {
                    return 1;
                }
            }
        }
        public static long HomeAppliancesCategoryId
        {
            get
            {
                try
                {
                    return long.Parse(ReadConfig("HomeAppliancesCategoryId"));
                }
                catch (Exception)
                {
                    return 1;
                }
            }
        }
        public static long LaptopAndComputerCategoryId
        {
            get
            {
                try
                {
                    return long.Parse(ReadConfig("LaptopAndComputerCategoryId"));
                }
                catch (Exception)
                {
                    return 1;
                }
            }
        }
        public static long MobileCategoryId
        {
            get
            {
                try
                {
                    return long.Parse(ReadConfig("MobileCategoryId"));
                }
                catch (Exception)
                {
                    return 1;
                }
            }
        }
        public static long CategoryIdForHome
        {
            get
            {
                try
                {
                    return long.Parse(ReadConfig("CategoryIdForHome"));
                }
                catch (Exception)
                {
                    return 1;
                }
            }
        }



    }
}
