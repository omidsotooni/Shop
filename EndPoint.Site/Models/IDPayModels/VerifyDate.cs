using Shop.Common;

namespace EndPoint.Site.Models.IDPayModels
{
    public class VerifyDate
    {
        public double date { get; set; }

        public DateTime Date
        {
            get
            {
                return Utility.UnixTimeStampToDateTime(date);
            }
        }
    }

}
