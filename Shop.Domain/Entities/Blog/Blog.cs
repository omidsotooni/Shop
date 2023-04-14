using Shop.Domain.Entities.Commons;
using Shop.Domain.Entities.Languages;
using Shop.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;
using static Shop.Common.Utility;

namespace Shop.Domain.Entities.Blog
{
    public class BlogEntity : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Slug { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public BlogStatus BlogStatus { get; set; } = BlogStatus.Draft;
        public string PictureSrc { get; set; } = string.Empty;
        public bool IsIndexed { get; set; } = false;
        public bool IsFollowed { get; set; } = false;
        public List<string> Tags { get; set; } = new List<string>();
        public string Description { get; set; } = string.Empty;
        public long? LanguageId { get; set; }
        public virtual Language Language { get; set; } = new Language();
        public long BlogCategoryId { get; set; }
        public virtual BlogCategory BlogCategory { get; set; } = new BlogCategory();
        public virtual ICollection<FAQBlog> FAQBlogs { get; set; }
        public string UrlRedirect { get; set; } = String.Empty;
        public string VideoUrl { get; set; } = String.Empty;
        public string Canonical { get; set; } = String.Empty;
        public virtual User User { get; set; } = new User();
        public long UserId { get; set; }
        public long ViewCount { get; set; }
        public int ReadingTime { get; set; }
    }
}
