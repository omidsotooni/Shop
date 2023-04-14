using Shop.Common.Dto;
using Shop.Domain.Entities.Blog;

namespace Shop.Application.Services.Blog.Queries
{
    public interface IGetBlogServices
    {
        Task<ResultDto<BlogEntity>> GetBlogById(long blogId);
        Task<ResultDto<BlogEntity>> GetBlogBySlug(string slug);
        Task<ResultDto<BlogCategory>> GetBlogCategory(long blogCategoryId);
        ResultDto<List<AllBlogCategoriesDto>> GetAllBlogCategories();
        Task<ResultDto<List<FAQBlog>>> GetFAQBlogList(long blogId);
    }
}
