using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Xml.Linq;

namespace Shop.Common
{
    public static class Utility
    {
        #region Methods
        /// <summary>
        /// Pagination
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowsCount"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> ToPaged<TSource>(this IEnumerable<TSource> source, int page, int pageSize, out int rowsCount)
        {
            rowsCount = source.Count();
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }
        /// <summary>
        /// Upload Image Files
        /// </summary>
        /// <param name="file"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static UploadDto UploadFile(IFormFile file, IHostingEnvironment environment)
        {
            if (file != null)
            {
                string folder = $@"images\HomePages\Slider\";
                var uploadsRootFolder = Path.Combine(environment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }
                if (file == null || file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }
                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return new UploadDto()
                {
                    FileNameAddress = folder + fileName,
                    Status = true,
                };
            }
            return null;
        }

        public static string ExceptionMessage(Exception ex)
        {
            string str = "Error From Server: ";
            if (!string.IsNullOrEmpty(ex.Message))
                str += ex.Message;
            return str;
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public static string GetStatusIDPay(int status)
        {
            switch (status)
            {
                case 1: return "پرداخت انجام نشده است";
                case 2: return "پرداخت ناموفق بوده است";
                case 3: return "خطا رخ داده است";
                case 4: return "بلوکه شده";
                case 5: return "برگشت به پرداخت کننده";
                case 6: return "برگشت خورده سیستمی";
                case 10: return "در انتظار تایید پرداخت";
                case 100: return "پرداخت تایید شده است";
                case 101: return "پرداخت قبلا تایید شده است";
                case 200: return "به دریافت کننده واریز شد";
                default: return "";
            }
        }
        public enum Banking
        {
            /// <summary>
            /// IDPay
            /// </summary>
            [Display(Name = "درگاه آیدی پی")]
            IDPay = 0,
            /// <summary>
            /// Zarinpal
            /// </summary>
            [Display(Name = "درگاه زرین پال")]
            Zarinpal = 1,
        }
        public enum OrderState
        {
            /// <summary>
            /// سفارش ناموفق
            /// </summary>
            [Display(Name = "سفارش ناموفق")]
            UnSuccess = 0,
            /// <summary>
            /// سفارش در حال پردازش
            /// </summary>
            [Display(Name = "در حالی پردازش")]
            Processing = 1,
            /// <summary>
            /// سفارش لغو شده توسط کاربر
            /// </summary>
            [Display(Name = "لغو شده")]
            Canceled = 2,
            /// <summary>
            /// سفارش تحویل شده به کاربر
            /// </summary>
            [Display(Name = "تحویل شده")]
            Delivered = 3,
            /// <summary>
            /// سفارش موفق
            /// </summary>
            [Display(Name = "سفارش موفق")]
            Success = 4,
        }
        public static string KhorshidiDate(DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            string date = string.Format("{0}/{1}/{2}", pc.GetYear(dateTime),
                pc.GetMonth(dateTime), pc.GetDayOfMonth(dateTime));
            date += " ساعت ";
            date += string.Format("{0}:{1}", pc.GetHour(dateTime), pc.GetMinute(dateTime));
            return date;
        }
        public static string ConvertDateToKhorshidi(DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            return string.Format("{0}/{1}/{2}T{3}:{4}", pc.GetYear(dateTime),
                pc.GetMonth(dateTime), pc.GetDayOfMonth(dateTime),
                pc.GetHour(dateTime), pc.GetMinute(dateTime));
        }
        #endregion
    }
    public class UploadDto
    {
        public long Id { get; set; }
        public bool Status { get; set; }
        public string FileNameAddress { get; set; }
    }
}
