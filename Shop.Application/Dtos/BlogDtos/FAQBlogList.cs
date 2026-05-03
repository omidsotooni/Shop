namespace Shop.Application.Dtos.BlogDtos
{
    public class FAQBlogList
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
    public class FAQBlogListForEdit
    {
        public long Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
