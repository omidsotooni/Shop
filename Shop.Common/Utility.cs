using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace Shop.Common
{
    public static class Utility
    {
        public static readonly string Dash = "-";
        public static readonly string Space = " ";
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
        public static UploadDto UploadFile(IFormFile file, IHostingEnvironment environment, string imageFor)
        {
            if (file != null)
            {
                string folder = $@"images\" + imageFor;
                if (imageFor == "HomePages")
                {
                    folder += @"\Slider\";
                }
                else
                {
                    folder += @"\" + imageFor + @"\";
                }
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
        public enum BlogStatus
        {
            /// <summary>
            /// وبلاگ های غیرفعال
            /// </summary>
            [Display(Name = "منتشر نشده")]
            NotPublished = 0,
            /// <summary>
            /// وبلاگ هایی که در سایت نمایش داده می شوند
            /// </summary>
            [Display(Name = "منتشر شده")]
            Published = 1,
            /// <summary>
            /// وبلاگ های پیش نویس که هنوز آماده نشده اند
            /// </summary>
            [Display(Name = "پیش نویس")]
            Draft = 2,
            /// <summary>
            /// وبلاگ هایی که وضعیت آنها مشخص نیست
            /// </summary>
            [Display(Name = "نامشخص")]
            Unknown = 3
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
        public static string KhorshidiJustDate(DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            string date = string.Format("{0}/{1}/{2}", pc.GetYear(dateTime),
                pc.GetMonth(dateTime), pc.GetDayOfMonth(dateTime));            
            return date;
        }
        public static string KhorshidiDateNull(DateTime? dateTime)
        {
            if (dateTime == null)
                dateTime = DateTime.Now;
            PersianCalendar pc = new PersianCalendar();
            string date = string.Format("{0}/{1}/{2}", pc.GetYear((DateTime)dateTime),
                pc.GetMonth((DateTime)dateTime), pc.GetDayOfMonth((DateTime)dateTime));
            date += " ساعت ";
            date += string.Format("{0}:{1}", pc.GetHour((DateTime)dateTime), pc.GetMinute((DateTime)dateTime));
            return date;
        }
        public static string ConvertDateToKhorshidi(DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            return string.Format("{0}/{1}/{2}T{3}:{4}", pc.GetYear(dateTime),
                pc.GetMonth(dateTime), pc.GetDayOfMonth(dateTime),
                pc.GetHour(dateTime), pc.GetMinute(dateTime));
        }
        public static string CreateSlug(string title)
        {
            title = title.Trim()?.ToLowerInvariant().Replace(Space, Dash, StringComparison.OrdinalIgnoreCase) ?? string.Empty;
            title = RemoveDiacritics(title);
            title = RemoveReservedUrlCharacters(title);

            return title.ToLowerInvariant();
        }
        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        private static string RemoveReservedUrlCharacters(string text)
        {
            var reservedCharacters = new List<string> { "!", "#", "$", "&", "'", "(", ")", "*", ",", "/", ":", ";", "=", "?", "@", "[", "]", "\"", "%", ".", "<", ">", "\\", "^", "_", "'", "{", "}", "|", "~", "`", "+" };

            foreach (var chr in reservedCharacters)
            {
                text = text.Replace(chr, string.Empty, StringComparison.OrdinalIgnoreCase);
            }

            return text;
        }

        public enum Languages
        {
            /// <summary>
            /// انگلیسی
            /// </summary>
            [Display(Name = "انگلیسی")]
            English = 0,
            /// <summary>
            /// فارسی
            /// </summary>
            [Display(Name = "فارسی")]
            Persian = 1,
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
