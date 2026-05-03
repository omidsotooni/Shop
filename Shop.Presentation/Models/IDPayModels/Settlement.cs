using Shop.Common;

namespace EndPoint.Site.Models.IDPayModels
{
    public class Settlement
    {
        public string track_id { get; set; }
        public decimal amount { get; set; }
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
