namespace Shop.Infrastructure.Dtos.BlogDtos
{
    public class ResultBlogForSiteListDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<BlogsForSitetDto> Blogs { get; set; }
    }
}
