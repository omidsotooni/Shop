using static Shop.Common.Utility;

namespace Shop.Infrastructure.Dtos.BlogDtos
{
    public class BlogsFormAdminListDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public BlogStatus BlogStatus { get; set; }
        public bool IsIndexed { get; set; } = false;
        public bool IsFollowed { get; set; } = false;
        public string Tags { get; set; }
        public string Description { get; set; }
        public string LanguageTitle { get; set; }
        public string CategoryText { get; set; }
        public string PictureSrc { get; set; }
        public string UrlRedirect { get; set; }
        public string VideoUrl { get; set; }
        public string Canonical { get; set; } 
        public long ViewCount { get; set; }
        public int ReadingTime { get; set; }
    }
}
