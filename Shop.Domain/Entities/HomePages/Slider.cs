using Shop.Domain.Entities.Commons;

namespace Shop.Domain.Entities.HomePages
{
    public class Slider : BaseEntity
    {
        public string Src { get; set; }
        public string Link { get; set; }
        public string AltName { get; set; }
    }
}
