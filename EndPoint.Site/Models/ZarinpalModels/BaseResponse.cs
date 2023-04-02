namespace EndPoint.Site.Models.ZarinpalModels
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        // I Personally never received an error, So although we know it is a list, it's not guaranteed to be a list of `string`
        public List<string> Errors { get; set; }
    }
}
