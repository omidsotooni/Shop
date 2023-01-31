using Shop.Application.Services.Products.Commands.AddNewCategory;
using Shop.Application.Services.Products.Queries.GetCategories;

namespace Shop.Application.Interfaces.FacadPatterns
{
    public interface IProductFacad
    {
        AddNewCategoryService AddNewCategoryService { get; }
        IGetCategoriesService GetCategoriesService { get; }
    }
}
