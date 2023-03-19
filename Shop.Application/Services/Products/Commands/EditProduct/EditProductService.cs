using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Contexts;
using Shop.Application.Services.Products.Commands.AddNewProduct;
using Shop.Common.Dto;
using Shop.Domain.Entities.Products;
using System;

namespace Shop.Application.Services.Products.Commands.EditProduct
{
    public class EditProductService : IEditProductService
    {
        #region Fields
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        #endregion

        #region Constructor
        public EditProductService(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
                if (product == null)
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
            using var transaction = _context.BeginTransaction();
            try
            {
                var p = _context.Products.Find(product.Id);
                if (p == null)
                {
                    return new ResultDto<EditProduct>()
                    {
                        IsSuccess = false,
                        Message = "محصول مورد نظر پیدا نشد!"
                    };
                }
                p.Name = product.Name;
                p.Inventory = product.Inventory;
                p.Price = product.Price;
                p.Description = product.Description;
                p.CategoryId = product.CategoryId;
                p.Displayed = product.Displayed;
                p.Brand = product.Brand;
                p.UpdateTime = DateTime.Now;
                if(product.MoreImages != null && product.MoreImages.Count() > 0)
                {
                    List<ProductImages> productImages = new List<ProductImages>();
                    foreach (var item in product.MoreImages)
                    {
                        var uploadedResult = UploadFile(item);
                        productImages.Add(new ProductImages
                        {
                            Product = p,
                            Src = uploadedResult.FileNameAddress,
                        });
                    }
                    _context.ProductImages.AddRange(productImages);
                }

                _context.SaveChanges();
                transaction.Commit();

                return new ResultDto<EditProduct>()
                {
                    IsSuccess = true,
                    Message = "محصول مورد نظر ویرایش شد!"
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
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

        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\ProductImages\";
                var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }

                if (file == null || file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }

                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return new UploadDto()
                {
                    FileNameAddress = folder + fileName,
                    Status = true,
                };
            }
            return null;
        }
        #endregion
    }
}
