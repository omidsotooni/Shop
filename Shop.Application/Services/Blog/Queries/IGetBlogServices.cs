using Shop.Common.Dto;
using Shop.Domain.Entities.Blog;
using Shop.Infrastructure.Dtos.BlogDtos;

namespace Shop.Application.Services.Blog.Queries
{
    public interface IGetBlogServices
    {
        ResultDto<BlogForAdminDto> GetBlogs(int Page = 1, int PageSize = 20);
        Task<ResultDto<BlogEntity>> GetBlogById(long blogId);
        ResultDto<DetailBlogDto> GetBlogBySlug(string slug);
        ResultDto<ResultBlogForSiteListDto> GetBlogsForSite(string SearchKey, int Page, int PageSize);
        ResultDto<List<AllBlogForAddFAQDto>> GetAllBlogForAddFAQ();
        Task<ResultDto<BlogCategory>> GetBlogCategory(long blogCategoryId);
        ResultDto<List<AllBlogCategoriesDto>> GetAllBlogCategories();
        ResultDto<EditBlogDto> GetBlogByIdForEdit(long blogId);        
        ResultDto<FAQBlog> GetFAQByIdForEdit(long FAQId);
        ResultDto<FAQForAdminDto> GetFAQBlogList(int Page = 1, int PageSize = 20);
    }
}
