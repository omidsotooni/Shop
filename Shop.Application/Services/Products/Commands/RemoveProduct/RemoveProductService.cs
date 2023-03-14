using Shop.Application.Interfaces.Contexts;
using Shop.Common.Dto;

namespace Shop.Application.Services.Products.Commands.RemoveProduct
{
    public class RemoveProductService: IRemoveProductService
    {
        private readonly IDataBaseContext _context;
        public RemoveProductService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long ProductId)
        {
            try
            {
                var product = _context.Products.Find(ProductId);
                if (product == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "محصول مورد نظر پیدا نشد!"
                    };
                }
                product.RemoveTime = DateTime.Now;
                product.IsRemoved = true;
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "محصول مورد نظر حذف شد"
                };
            }
            catch (Exception ex)
            {
                string str = "Error From Server: ";
                if (!string.IsNullOrEmpty(ex.Message)) 
                    str += ex.Message;
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "محصول مورد نظر پیدا نشد!"
                };
            }
        }       

    }
}
