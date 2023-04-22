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

        public ResultDto<DetailBlogDto> GetBlogBySlug(string slug)
        {
            try
            {
                var blog = _context.BlogEntities.Where(b => b.Slug == slug).Include(cat => cat.BlogCategory)
                    .Include(lang => lang.Language).Include(usr => usr.User).Include(faq => faq.FAQBlogs)
                    .FirstOrDefault();
                if (blog == null)
                {
                    return new ResultDto<DetailBlogDto>()
                    {
                        IsSuccess = false,
                        Message = "مطلب مورد نظر پیدا نشد!"
                    };
                }
                blog.ViewCount++;
                _context.SaveChanges();

                return new ResultDto<DetailBlogDto>()
                {
                    Data = new DetailBlogDto
                    {
                        Canonical = blog.Canonical,
                        CategoryText = blog.BlogCategory.CategoryText,
                        Content = blog.Content,
                        Description = blog.Description,
                        FAQBlogs = blog.FAQBlogs.Select(x => new FAQBlogDetail
                        {
                            Answer = x.Answer,
                            Question = x.Question,
                            Id = x.Id,
                        }).ToList(),
                        IsFollowed = blog.IsFollowed,
                        IsIndexed = blog.IsIndexed,
                        LanguageTitle = blog.Language.Title,
                        PictureSrc = blog.PictureSrc,
                        ReadingTime = blog.ReadingTime,
                        Slug = blog.Slug,
                        Tags = string.Join(",", blog.Tags),
                        Title = blog.Title,
                        UrlRedirect = blog.UrlRedirect,
                        UserName = blog.User.FullName,
                        AuthorUrl = blog.User.UserUrl,
                        VideoUrl = blog.VideoUrl,
                        ViewCount = blog.ViewCount,
                        DatePublished = blog.InsertTime,
                        LastModified = blog.UpdateTime == null ? DateTime.Now : blog.UpdateTime,
                    },
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                Utility.ExceptionMessage(ex);
                return new ResultDto<DetailBlogDto>()
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
                if (blogCategory == null)
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

        public ResultDto<EditBlogDto> GetBlogByIdForEdit(long blogId)
        {
            try
            {
                var blog = _context.BlogEntities.Include(x => x.Language).Include(x => x.BlogCategory)
                    .Include(x => x.FAQBlogs).Include(x => x.User).Where(o => o.Id == blogId).FirstOrDefault();
                if (blog is null)
                {
                    return new ResultDto<EditBlogDto>()
                    {
                        IsSuccess = false,
                        Message = "پست مورد نظر پیدا نشد!",
                    };
                }
                return new ResultDto<EditBlogDto>()
                {
                    Data = new EditBlogDto()
                    {
                        Id = blog.Id,
                        BlogCategoryId = blog.BlogCategoryId,
                        CategoryText = blog.BlogCategory.CategoryText,
                        BlogStatus = blog.BlogStatus,
                        Slug = blog.Slug,
                        PictureSrc = blog.PictureSrc,
                        Canonical = blog.Canonical,
                        Content = blog.Content,
                        Description = blog.Description,
                        IsFollowed = blog.IsFollowed,
                        IsIndexed = blog.IsIndexed,
                        LanguageId = (long)(blog.LanguageId == null ? 1 : blog.LanguageId),
                        Tags = string.Join(",", blog.Tags),
                        Title = blog.Title,
                        UserId = blog.UserId,
                        ReadingTime = blog.ReadingTime,
                        UrlRedirect = blog.UrlRedirect,
                        VideoUrl = blog.VideoUrl,
                        ViewCount = blog.ViewCount,
                        FAQBlogs = blog.FAQBlogs.ToList().Select(o => new FAQBlogListForEdit()
                        {
                            Id = o.Id,
                            Answer = o.Answer,
                            Question = o.Question,

                        }).ToList(),
                    },
                    IsSuccess = true,
                    Message = "",
                };
            }
            catch (Exception ex)
            {
                Utility.ExceptionMessage(ex);
                return new ResultDto<EditBlogDto>()
                {
                    IsSuccess = false,
                };
            }
        }

        public ResultDto<BlogForAdminDto> GetBlogs(int Page = 1, int PageSize = 20)
        {
            try
            {
                int rowCount = 0;
                var blogs = _context.BlogEntities.Include(x => x.Language).Include(x => x.BlogCategory)
                    .Include(x => x.FAQBlogs).ToPaged(Page, PageSize, out rowCount)
                    .Select(o => new BlogsFormAdminListDto
                    {
                        Id = o.Id,
                        BlogStatus = o.BlogStatus,
                        Canonical = o.Canonical,
                        CategoryText = o.BlogCategory.CategoryText,
                        Content = o.Content,
                        Description = o.Description,
                        IsFollowed = o.IsFollowed,
                        IsIndexed = o.IsIndexed,
                        LanguageTitle = o.Language.Title,
                        PictureSrc = o.PictureSrc,
                        ReadingTime = o.ReadingTime,
                        Slug = o.Slug,
                        Tags = string.Join(",", o.Tags),
                        Title = o.Title,
                        UrlRedirect = o.UrlRedirect,
                        VideoUrl = o.VideoUrl,
                        ViewCount = o.ViewCount,
                    }).ToList();
                return new ResultDto<BlogForAdminDto>()
                {
                    Data = new BlogForAdminDto()
                    {
                        Blogs = blogs,
                        CurrentPage = Page,
                        PageSize = PageSize,
                        RowCount = rowCount
                    },
                    IsSuccess = true,
                    Message = "",
                };
            }
            catch (Exception ex)
            {
                Utility.ExceptionMessage(ex);
                return new ResultDto<BlogForAdminDto>()
                {
                    IsSuccess = false,
                };
            }
        }

        public ResultDto<ResultBlogForSiteListDto> GetBlogsForSite(string SearchKey, int Page, int PageSize)
        {
            try
            {
                int rowCount = 0;
                var blogQuery = _context.BlogEntities.Where(o => o.BlogStatus == Utility.BlogStatus.Published)
                    .Include(o => o.BlogCategory).Include(o => o.User).AsQueryable();
                if (!string.IsNullOrWhiteSpace(SearchKey))
                {
                    blogQuery = blogQuery.Where(x => x.Title.Contains(SearchKey) || x.Description.Contains(SearchKey)
                    || x.BlogCategory.CategoryText.Contains(SearchKey) || x.Content.Contains(SearchKey) 
                    || x.Tags.Contains(SearchKey)).AsQueryable();
                }
                var AllBlogs = blogQuery.ToPaged(Page, PageSize, out rowCount)
                    .Select(o => new BlogsForSitetDto
                    {
                        Id = o.Id,
                        Slug = o.Slug,
                        Title = o.Title,
                        PictureSrc = o.PictureSrc,
                        CategoryText = o.BlogCategory.CategoryText,
                        ViewCount = o.ViewCount,
                        Description = o.Description,
                        InsertDate = o.InsertTime,
                        UserName = o.User.FullName,
                        AuthorUrl = o.User.UserUrl,
                    }).OrderByDescending(o => o.InsertDate).ToList();

                return new ResultDto<ResultBlogForSiteListDto>()
                {
                    Data = new ResultBlogForSiteListDto()
                    {
                        Blogs = AllBlogs,
                        CurrentPage = Page,
                        PageSize = PageSize,
                        RowCount = rowCount,
                    },
                    IsSuccess = true,
                    Message = "",
                };
            }
            catch (Exception ex)
            {
                Utility.ExceptionMessage(ex);
                return new ResultDto<ResultBlogForSiteListDto>()
                {
                    IsSuccess = false,
                };
            }
        }


        #endregion
    }
}
