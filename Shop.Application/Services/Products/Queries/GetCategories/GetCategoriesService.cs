using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Contexts;
using Shop.Common.Dto;
using System;

namespace Shop.Application.Services.Products.Queries.GetCategories
{
    public class GetCategoriesService : IGetCategoriesService
    {
        private readonly IDataBaseContext _context;

        public GetCategoriesService(IDataBaseContext context)
        {
            _context = context;
        }

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
    }
}
