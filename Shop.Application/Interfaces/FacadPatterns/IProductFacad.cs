using Microsoft.EntityFrameworkCore;
using Shop.Application.Services.Products.Commands.AddNewCategory;
using Shop.Application.Services.Products.Commands.AddNewProduct;
using Shop.Application.Services.Products.Commands.EditCategory;
using Shop.Application.Services.Products.Commands.EditProduct;
using Shop.Application.Services.Products.Commands.RemoveCategory;
using Shop.Application.Services.Products.Commands.RemoveProduct;
using Shop.Application.Services.Products.Queries.GetAllCategories;
using Shop.Application.Services.Products.Queries.GetCategories;
using Shop.Application.Services.Products.Queries.GetProductDetailForAdmin;
using Shop.Application.Services.Products.Queries.GetProductForAdmin;

namespace Shop.Application.Interfaces.FacadPatterns
{
    public interface IProductFacad
    {
        AddNewCategoryService AddNewCategoryService { get; }
        IGetCategoriesService GetCategoriesService { get; }
        AddNewProductService AddNewProductService { get; }
        IGetAllCategoriesService GetAllCategoriesService { get; }
        /// <summary>
        /// Get Product Lists
        /// </summary>
        IGetProductForAdminService GetProductForAdminService { get; }
        IGetProductDetailForAdminService GetProductDetailForAdminService { get; }

        /// <summary>
        /// Remove Product
        /// </summary>
        IRemoveProductService RemoveProductService { get; }

        /// <summary>
        /// Remove Category
        /// </summary>
        IRemoveCategoryService RemoveCategoryService { get; }
        /// <summary>
        /// Edit Category
        /// </summary>
        IEditCategoryService EditCategoryService { get; }
        /// <summary>
        /// Edit Product
        /// </summary>
        IEditProductService EditProductService { get; }

    }
}
