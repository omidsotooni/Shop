namespace Shop.Application.Services.Products.Commands.EditProduct
{
    public class EditProduct
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }
        /*public List<IFormFile> Images { get; set; }*/
        public List<EditProductFeature> Features { get; set; }
        public List<EditProductImages> Images { get; set; }
    }
    public class EditProductFeature
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
    public class EditProductImages
    {
        public long Id { get; set; }
        public string Src { get; set; }
    }
}
