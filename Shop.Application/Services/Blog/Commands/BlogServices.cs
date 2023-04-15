using Shop.Application.Interfaces.Contexts;
using Shop.Common.Dto;
using Shop.Infrastructure.Dtos.BlogDtos;
using Microsoft.AspNetCore.Hosting;
using Shop.Common;
using Shop.Domain.Entities.Blog;
using Shop.Domain.Entities.Languages;
using static Shop.Common.Utility;

namespace Shop.Application.Services.Blog.Commands
{
    public class BlogServices : IBlogServices
    {
        #region Fields
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        #endregion

        #region Constructor
        public BlogServices(IDataBaseContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _environment = hostingEnvironment;
        }

        #endregion

        #region Methods
        public ResultDto AddNewBlog(AddNewBlogDto addNewBlogDto)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                string ImageFor = "Blog";
                var blog = _context.BlogEntities.FirstOrDefault(e => e.Slug == addNewBlogDto.Slug);
                if (blog is not null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "آدرس مطلب مورد نظر تکراری است، لطفا آدرس را تغییر دهید.",
                    };
                }
                var user = _context.Users.Find(addNewBlogDto.UserId);
                if (user is null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "نویسنده پیدا نشد، مطلب بدون نام نویسنده قابل ذخیره نیست",
                    };
                }
                var blogCategory = _context.BlogCategories.Find(addNewBlogDto.BlogCategoryId);
                if (blogCategory is null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "دسته بندی‌ای برای مطلب پیدا نشد، لطفا دسته بندی را انتخاب کنید.",
                    };
                }
                var imageAddress = Utility.UploadFile(addNewBlogDto.BlogImages.First(), _environment, ImageFor).FileNameAddress;
                var lang = _context.Languages.FirstOrDefault(o => o.Title.ToLower() == ((Utility.Languages)addNewBlogDto.LanguageId).ToString().ToLower());
                if (lang is null)
                {
                    Language language = new Language()
                    {
                        Title = ((Utility.Languages)addNewBlogDto.LanguageId).ToString(),
                    };
                    _context.Languages.AddRange(language);
                    addNewBlogDto.LanguageId = language.Id;
                    addNewBlogDto.LanguageTitle = language.Title;
                    lang = language;
                }
                BlogEntity blogEntity = new BlogEntity()
                {
                    Slug = !string.IsNullOrWhiteSpace(addNewBlogDto.Slug) ? Utility.CreateSlug(addNewBlogDto.Slug) : Utility.CreateSlug(addNewBlogDto.Title),
                    Title = addNewBlogDto.Title,
                    Description = addNewBlogDto.Description,
                    Content = addNewBlogDto.Content,
                    User = user,
                    BlogCategory = blogCategory,
                    BlogCategoryId = blogCategory.Id,
                    BlogStatus = addNewBlogDto.BlogStatus,
                    Canonical = addNewBlogDto.Canonical,
                    LanguageId = addNewBlogDto.LanguageId,
                    Language = lang,
                    ViewCount = 1,
                    VideoUrl = addNewBlogDto.VideoUrl,
                    ReadingTime = addNewBlogDto.ReadingTime,
                    UrlRedirect = addNewBlogDto.UrlRedirect,
                    IsFollowed = addNewBlogDto.IsFollowed,
                    IsIndexed = addNewBlogDto.IsIndexed,
                    PictureSrc = imageAddress,
                };
                if (addNewBlogDto.Tags is not null)
                {
                    //blogEntity.Tags = new List<string>();
                    addNewBlogDto.Tags.Split(",", StringSplitOptions.RemoveEmptyEntries)
                        .Select(t => t.Trim().ToLowerInvariant()).ToList().ForEach(blogEntity.Tags.Add);
                }
                _context.BlogEntities.Add(blogEntity);
                List<FAQBlog> fAQBlogs = new List<FAQBlog>();
                foreach (var item in addNewBlogDto.FAQBlogs)
                {
                    fAQBlogs.Add(new FAQBlog
                    {
                        Question = item.Question,
                        Answer = item.Answer,
                        BlogEntity = blogEntity,
                    });
                }
                _context.FAQBlogs.AddRange(fAQBlogs);
                _context.SaveChanges();
                transaction.Commit();

                var blogState = EnumHelpers<BlogStatus>.GetDisplayValue(blogEntity.BlogStatus);
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = $"مطلب مورد نظر بصورت {blogState} ذخیره شد",
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Utility.ExceptionMessage(ex);
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مطلب مورد نظر ثبت نشد!"
                };
            }
        }

        public ResultDto AddNewBlogCategory(string CategoryText)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(CategoryText))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "نام دسته بندی مطالب را وارد نمایید",
                    };
                }
                BlogCategory blogCategory = new BlogCategory()
                {
                    CategoryText = CategoryText,
                };
                _context.BlogCategories.Add(blogCategory);
                _context.SaveChanges();
                transaction.Commit();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "دسته بندی مطالب با موفقیت اضافه شد",
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Utility.ExceptionMessage(ex);
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "دسته بندی ثبت نشد!"
                };
            }
        }

        public ResultDto DeleteBlogCategory(long blogCategoryId)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                var blogCategory = _context.BlogCategories.Find(blogCategoryId);
                if (blogCategory is null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "دسته بندی مطالب پیدا نشد!"
                    };
                }
                blogCategory.RemoveTime = DateTime.Now;
                blogCategory.IsRemoved = true;
                _context.SaveChanges();
                transaction.Commit();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "دسته بندی مطالب حذف شد!"
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Utility.ExceptionMessage(ex);
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "دسته بندی ثبت نشد!"
                };
            }
        }

        public ResultDto EditBlogCategory(BlogCategory blogCategory)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                var category = _context.BlogCategories.Find(blogCategory.Id);
                if (category is null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "دسته بندی مطالب پیدا نشد!"
                    };
                }
                category.UpdateTime = DateTime.Now;
                category.CategoryText = blogCategory.CategoryText;
                _context.SaveChanges();
                transaction.Commit();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "دسته بندی مطالب ویرایش شد!"
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Utility.ExceptionMessage(ex);
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "دسته بندی ثبت نشد!"
                };
            }
        }

        public ResultDto<EditBlogDto> EditBlog(EditBlogDto editBlogDto)
        {
            string ImageFor = "Blog";
            using var transaction = _context.BeginTransaction();
            try
            {
                var blog = _context.BlogEntities.Find(editBlogDto.Id);
                if (blog is null)
                {
                    return new ResultDto<EditBlogDto>()
                    {
                        IsSuccess = false,
                        Message = "مطلب مورد نظر پیدا نشد!",
                    };
                }
                blog.UpdateTime = DateTime.Now;
                blog.BlogCategoryId = editBlogDto.BlogCategoryId;
                blog.BlogStatus = editBlogDto.BlogStatus;
                blog.Content = editBlogDto.Content;
                blog.Title = editBlogDto.Title;
                blog.Description = editBlogDto.Description;
                blog.UserId = editBlogDto.UserId; // ClaimUtility.GetUserId(User);                
                if (editBlogDto.Tags is not null)
                {
                    blog.Tags.Clear();
                    editBlogDto.Tags.Split(",", StringSplitOptions.RemoveEmptyEntries)
                        .Select(t => t.Trim().ToLowerInvariant()).ToList().ForEach(blog.Tags.Add);
                }
                if (editBlogDto.Canonical is not null)
                {
                    blog.Canonical = editBlogDto.Canonical;
                }
                if (editBlogDto.VideoUrl is not null)
                {
                    blog.VideoUrl = editBlogDto.VideoUrl;
                }
                if (editBlogDto.UrlRedirect is not null)
                {
                    blog.UrlRedirect = editBlogDto.UrlRedirect;
                }
                blog.Slug = !string.IsNullOrWhiteSpace(editBlogDto.Slug) ? Utility.CreateSlug(editBlogDto.Slug) : Utility.CreateSlug(editBlogDto.Title);                
                blog.LanguageId = editBlogDto.LanguageId;
                blog.ReadingTime = editBlogDto.ReadingTime;
                blog.IsFollowed = editBlogDto.IsFollowed;
                blog.IsIndexed = editBlogDto.IsIndexed;
                if (editBlogDto.NewBlogImages is not null && editBlogDto.NewBlogImages.Count() > 0)
                {
                    editBlogDto.PictureSrc = Utility.UploadFile(editBlogDto.NewBlogImages.First(), _environment, ImageFor).FileNameAddress;
                    blog.PictureSrc = editBlogDto.PictureSrc;
                }
                _context.SaveChanges();
                transaction.Commit();

                var blogState = EnumHelpers<BlogStatus>.GetDisplayValue(editBlogDto.BlogStatus);
                return new ResultDto<EditBlogDto>()
                {
                    IsSuccess = true,
                    Message = $"مطلب مورد نظر با وضعیت {blogState} ویرایش شد",
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Utility.ExceptionMessage(ex);
                return new ResultDto<EditBlogDto>()
                {
                    IsSuccess = false,
                    Message = "مطلب ویرایش نشد!",
                };
            }
        }

        public ResultDto DeleteBlog(long blogId)
        {
            using var transaction = _context.BeginTransaction();
            try
            {
                var blog = _context.BlogEntities.Find(blogId);
                if (blog is null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "مطلب مورد نظر پیدا نشد!",
                    };
                }
                blog.RemoveTime = DateTime.Now;
                blog.IsRemoved = true;
                _context.SaveChanges();
                transaction.Commit();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "مطلب مورد نظر حذف شد",
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Utility.ExceptionMessage(ex);
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مطلب حذف نشد!"
                };
            }
        }

        #endregion
    }
}