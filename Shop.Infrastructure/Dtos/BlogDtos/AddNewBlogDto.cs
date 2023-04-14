using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static Shop.Common.Utility;

namespace Shop.Infrastructure.Dtos.BlogDtos
{
    public class AddNewBlogDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public string Content { get; set; }
        public BlogStatus BlogStatus { get; set; } = BlogStatus.Draft;
        [Required]
        public bool IsIndexed { get; set; } = false;
        public bool IsFollowed { get; set; } = false;
        public string Tags { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public long LanguageId { get; set; }
        public string LanguageTitle { get; set; }

        [Required]
        public long BlogCategoryId { get; set; }
        public List<IFormFile> BlogImages { get; set; }
        public List<FAQBlogList> FAQBlogs { get; set; }
        public List<long> FAQBlogsId { get; set; }
        public string UrlRedirect { get; set; } = String.Empty;
        public string VideoUrl { get; set; } = String.Empty;
        public string Canonical { get; set; } = String.Empty;
        [Required]
        public long UserId { get; set; }
        public long ViewCount { get; set; }
        [Required]
        public int ReadingTime { get; set; }
    }

    public class FAQBlogList
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
