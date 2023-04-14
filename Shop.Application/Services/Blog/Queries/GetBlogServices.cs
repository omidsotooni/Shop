using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Contexts;
using Shop.Application.Services.Products.Queries.GetAllCategories;
using Shop.Common;
using Shop.Common.Dto;
using Shop.Domain.Entities.Blog;
using Shop.Infrastructure.Dtos.BlogDtos;

namespace Shop.Application.Services.Blog.Queries
{
    public class GetBlogServices : IGetBlogServices
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public GetBlogServices(IDataBaseContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods
        public async Task<ResultDto<BlogEntity>> GetBlogById(long blogId)
        {
            try
            {
                var blog = await _context.BlogEntities.FirstOrDefaultAsync(e => e.Id == blogId);
                if (blog == null)
                {
                    return new ResultDto<BlogEntity>()
                    {
                        IsSuccess = false,
                        Message = "مطلب مورد نظر پیدا نشد!"
                    };
                }
                return new ResultDto<BlogEntity>()
                {
                    IsSuccess = true,
                    Data = blog,
                };
            }
            catch (Exception ex)
            {
                Utility.ExceptionMessage(ex);
                return new ResultDto<BlogEntity>()
                {
                    IsSuccess = false,
                    Message = "مطلب مورد نظر پیدا نشد!"
                };
            }
        }

        public async Task<ResultDto<BlogEntity>> GetBlogBySlug(string slug)
        {
            try
            {
                var blog = await _context.BlogEntities.FirstOrDefaultAsync(e => e.Slug == slug);
                if(blog == null)
                {
                    return new ResultDto<BlogEntity>()
                    {
                        IsSuccess = false,
                        Message = "مطلب مورد نظر پیدا نشد!"
                    };
                }
                return new ResultDto<BlogEntity>()
                {
                    IsSuccess = true,
                    Data = blog,
                };
            }
            catch (Exception ex)
            {
                Utility.ExceptionMessage(ex);
                return new ResultDto<BlogEntity>()
                {
                    IsSuccess = false,
                    Message = "مطلب مورد نظر پیدا نشد!"
                };
            }
        }

        public async Task<ResultDto<BlogCategory>> GetBlogCategory(long blogCategoryId)
        {
            try
            {
                var blogCategory = await _context.BlogCategories.FirstOrDefaultAsync(o => o.Id == blogCategoryId);
                if(blogCategory == null)
                {
                    return new ResultDto<BlogCategory>()
                    {
                        IsSuccess = false,
                        Message = "دسته بندی‌ای برای مطلب پیدا نشد، لطفا دسته بندی را انتخاب کنید.",
                    };
                }
                return new ResultDto<BlogCategory>()
                {
                    Data = blogCategory,
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                Utility.ExceptionMessage(ex);
                return new ResultDto<BlogCategory>()
                {
                    IsSuccess = false,
                    Message = "دسته بندی پیدا نشد!",
                };
            }
        }       

        public async Task<ResultDto<List<FAQBlog>>> GetFAQBlogList(long blogId)
        {
            try
            {
                var faqBlog = _context.FAQBlogs.AsQueryable();
                var faqBlogList = await faqBlog.Where(x => x.BlogId == blogId).Select(o => new FAQBlog
                {
                    Question = o.Question,
                    Answer = o.Answer,
                }).ToListAsync();
                if (faqBlogList == null)
                {
                    return new ResultDto<List<FAQBlog>>()
                    {
                        IsSuccess = false,
                        Message = "لیست سوالات متداول خالی است"
                    };
                }
                return new ResultDto<List<FAQBlog>>()
                {
                    Data = faqBlogList,
                    IsSuccess = true,
                    Message = ""
                };
            }
            catch (Exception ex)
            {
                Utility.ExceptionMessage(ex);
                return new ResultDto<List<FAQBlog>>()
                {
                    IsSuccess = false,
                    Message = "نویسنده پیدا نشد!",
                };
            }
        }

        public ResultDto<List<AllBlogCategoriesDto>> GetAllBlogCategories()
        {
            try
            {
                var blogCategories = _context.BlogCategories.Select(o => new AllBlogCategoriesDto
                {
                    Id = o.Id,
                    Name = o.CategoryText,
                }).ToList();
                if (blogCategories is null)
                {
                    return new ResultDto<List<AllBlogCategoriesDto>>()
                    {
                        IsSuccess = false,
                        Message = "لیست دسته بندی مطلب ها خالی است"
                    };
                }
                return new ResultDto<List<AllBlogCategoriesDto>>()
                {
                    Data = blogCategories,
                    IsSuccess = true,
                    Message = ""
                };
            }
            catch (Exception ex)
            {
                Utility.ExceptionMessage(ex);
                return new ResultDto<List<AllBlogCategoriesDto>>()
                {
                    IsSuccess = false,
                    Message = "لیست دسته بندی مطلب ها خالی است"
                };
            }
        }

        #endregion
    }
}
