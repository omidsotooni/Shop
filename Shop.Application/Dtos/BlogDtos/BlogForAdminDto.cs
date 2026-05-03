namespace Shop.Application.Dtos.BlogDtos
{
    public class BlogForAdminDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<BlogsFormAdminListDto> Blogs { get; set; }
    }
}