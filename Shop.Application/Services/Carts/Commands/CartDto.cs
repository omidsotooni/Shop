namespace Shop.Application.Services.Carts.Commands
{
    public class CartDto
    {
        public int ProductCount { get; set; }
        public int SumAmount { get; set; }
        public long CartId { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }
}
