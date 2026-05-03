namespace Shop.Application.Dtos.BlogDtos
{
    public class DetailBlogDto
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string PictureSrc { get; set; }
        public string Tags { get; set; }
        public string CategoryText { get; set; }
        public string LanguageTitle { get; set; }
        public int ReadingTime { get; set; }
        public bool IsFollowed { get; set; } = false;
        public bool IsIndexed { get; set; } = false;
        public List<FAQBlogDetail> FAQBlogs { get; set; }
        public string UrlRedirect { get; set; }
        public string VideoUrl { get; set; }
        public string Canonical { get; set; }
        public long ViewCount { get; set; }
        public string UserName { get; set; }     
        public string AuthorUrl { get; set; }
        public DateTime DatePublished { get; set; }
        public DateTime? LastModified { get; set; }
    }
    public class FAQBlogDetail
    {
        public long Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
