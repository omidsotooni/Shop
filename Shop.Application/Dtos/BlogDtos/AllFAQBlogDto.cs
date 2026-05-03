namespace Shop.Application.Dtos.BlogDtos
{
    public class AllFAQBlogDto
    {
        public long Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public long BlogId { get; set; }
    }
}