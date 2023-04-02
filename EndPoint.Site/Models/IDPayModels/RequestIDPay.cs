namespace EndPoint.Site.Models.IDPayModels
{
    public class RequestIDPay
    {        
        private string OrderID;
        public string order_id
        {
            get
            {
                return OrderID;
            }
        }
        public decimal amount { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string mail { get; set; }
        public string desc { get; set; }
        public string callback { get; set; }

        public RequestIDPay(string _OrderID)
        {
            if (string.IsNullOrEmpty(_OrderID))
                OrderID = Guid.NewGuid().ToString();
            else
                OrderID = _OrderID;
        }
    }
}
