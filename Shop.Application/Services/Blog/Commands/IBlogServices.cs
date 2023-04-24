using Shop.Common.Dto;
using Shop.Domain.Entities.Blog;
using Shop.Infrastructure.Dtos.BlogDtos;

namespace Shop.Application.Services.Blog.Commands
{
    public interface IBlogServices
    {
        ResultDto AddNewBlog(AddNewBlogDto addNewBlogDto);
        ResultDto AddNewBlogCategory(string CategoryText);
        ResultDto DeleteBlogCategory(long blogCategoryId);
        ResultDto EditBlogCategory(BlogCategory blogCategory);
        ResultDto<EditBlogDto> EditBlog(EditBlogDto editBlogDto);
        ResultDto DeleteBlog(long blogId);
        ResultDto AddNewFAQ(string Question, string Answer, long BlogId);
        ResultDto EditFAQBlog(FAQBlog fAQBlog);
        ResultDto DeleteFAQBlog(long faqId);
    }
}
