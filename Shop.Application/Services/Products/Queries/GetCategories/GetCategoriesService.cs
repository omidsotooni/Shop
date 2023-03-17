using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Contexts;
using Shop.Common.Dto;
using Shop.Domain.Entities.Products;
using System;

namespace Shop.Application.Services.Products.Queries.GetCategories
{
    public class GetCategoriesService : IGetCategoriesService
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public GetCategoriesService(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public ResultDto<List<CategoriesDto>> Execute(long? ParentId)
        {
            try
            {
                var categories = _context.Categories
                   .Include(p => p.ParentCategory)
                   .Include(p => p.SubCategories)
                   .Where(p => p.ParentCategoryId == ParentId)
                   .ToList()
                   .Select(p => new CategoriesDto
                   {
                       Id = p.Id,
                       Name = p.Name,
                       Parent = p.ParentCategory != null ? new
                       ParentCategoryDto
                       {
                           Id = p.ParentCategory.Id,
                           name = p.ParentCategory.Name,
                       }
                       : null,
                       HasChild = p.SubCategories.Count() > 0 ? true : false,
                   }).ToList();


                return new ResultDto<List<CategoriesDto>>()
                {
                    Data = categories,
                    IsSuccess = true,
                    Message = "لیست باموقیت برگشت داده شد"
                };
            }
            catch (Exception ex)
            {
                string str = "Error From Server: ";
                if (!string.IsNullOrEmpty(ex.Message))
                    str += ex.Message;
                return new ResultDto<List<CategoriesDto>>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "لیست دسته بندی خالی است",
                };
            }
        }

        public ResultDto<Category> GetCategoryById(long CategoryId)
        {
            try
            {
                var category = _context.Categories.Include(o => o.ParentCategory)
                   .Include(o => o.SubCategories).Where(o => o.Id == CategoryId).FirstOrDefault();
                if (category == null)
                {
                    return new ResultDto<Category>()
                    {
                        IsSuccess = false,
                        Message = "دسته بندی مورد نظر پیدا نشد!"
                    };
                }
                return new ResultDto<Category>()
                {
                    Data = category,
                    IsSuccess = true,
                    Message = "دسته بندی مورد نظر پیدا شد."
                };
            }
            catch (Exception ex)
            {
                string str = "Error From Server: ";
                if (!string.IsNullOrEmpty(ex.Message))
                    str += ex.Message;
                return new ResultDto<Category>()
                {
                    IsSuccess = false,
                    Message = "دسته بندی مورد نظر پیدا نشد!"
                };
            }
        }

        public ResultDto<List<CategoriesDto>> GetCategories()
        {
            try
            {
                var categories = _context.Categories.AsQueryable();
                var categoryList = categories.Select(o => new CategoriesDto
                {
                    Id = o.Id,
                    Name = o.Name,
                }).OrderBy(o => o.Id).ToList();

                return new ResultDto<List<CategoriesDto>>()
                {
                    Data = categoryList,
                    IsSuccess = true,
                    Message = "لیست برگشت داده شد"
                };
            }
            catch (Exception ex)
            {
                string str = "Error From Server: ";
                if (!string.IsNullOrEmpty(ex.Message))
                    str += ex.Message;
                return new ResultDto<List<CategoriesDto>>()
                {
                    IsSuccess = false,
                    Message = "لیست دسته بندی خالی است",
                };
            }
        }
        #endregion
    }
}
