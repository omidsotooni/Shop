using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Contexts;
using Shop.Common.Dto;
using Shop.Domain.Entities.Products;

namespace Shop.Application.Services.Products.Commands.EditProduct
{
    public class EditProductService : IEditProductService
    {
        #region Fields
        private readonly IDataBaseContext _context;
        #endregion

        #region Constructor
        public EditProductService(IDataBaseContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods
        public ResultDto<EditProduct> GetProductById(long ProductId)
        {
            try
            {
                var product = _context.Products
                    .Include(p => p.Category)
                    .ThenInclude(p => p.ParentCategory)
                    .Include(p => p.ProductFeatures)
                    .Include(p => p.ProductImages)
                    .Where(p => p.Id == ProductId)
                    .FirstOrDefault();
                if(product == null)
                {
                    return new ResultDto<EditProduct>()
                    {
                        IsSuccess = false,
                        Message = "محصول مورد نظر پیدا نشد!"
                    };
                }
                return new ResultDto<EditProduct>()
                {
                    Data = new EditProduct()
                    {
                        Brand = product.Brand,
                        Category = GetCategory(product.Category),
                        CategoryId = product.CategoryId,
                        Description = product.Description,
                        Displayed = product.Displayed,
                        Id = product.Id,
                        Inventory = product.Inventory,
                        Name = product.Name,
                        Price = product.Price,
                        Features = product.ProductFeatures.ToList().Select(o => new EditProductFeature()
                        {
                            Id = o.Id,
                            DisplayName = o.DisplayName,
                            Value = o.Value
                        }).ToList(),
                        Images = product.ProductImages.ToList().Select(o => new EditProductImages()
                        {
                            Id = o.Id,
                            Src = o.Src,
                        }).ToList(),
                    },
                    IsSuccess = true,
                    Message = ""
                };
            }
            catch (Exception ex)
            {
                string str = "Error From Server: ";
                if (!string.IsNullOrEmpty(ex.Message))
                    str += ex.Message;
                return new ResultDto<EditProduct>()
                {
                    IsSuccess = false,
                    Message = "محصول مورد نظر پیدا نشد!"
                };
            }
        }
        public ResultDto<EditProduct> Execute(EditProduct product)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                string str = "Error From Server: ";
                if (!string.IsNullOrEmpty(ex.Message))
                    str += ex.Message;
                return new ResultDto<EditProduct>()
                {
                    IsSuccess = false,
                    Message = "محصول مورد نظر پیدا نشد!"
                };
            }
        }

        private string GetCategory(Category category)
        {
            string result = category.ParentCategory != null ? $"{category.ParentCategory.Name} - " : "";
            return result += category.Name;
        }
        #endregion
    }
}
