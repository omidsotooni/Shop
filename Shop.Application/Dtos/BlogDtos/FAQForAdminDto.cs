namespace Shop.Application.Dtos.BlogDtos
{
    public class FAQForAdminDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<AllFAQBlogDto> FAQsBlog { get; set; }
    }
}