using Shop.Domain.Entities.Commons;

namespace Shop.Domain.Entities.HomePages
{
    public class HomePageImages : BaseEntity
    {
        public string Src { get; set; }
        public string Link { get; set; }
        public string AltName { get; set; }
        public bool IsActive { get; set; } = false;
        public ImageLocation ImageLocation { get; set; }
    }
    public enum ImageLocation
    {
        /// <summary>
        /// Top Left One
        /// </summary>
        TopLeft_1 = 0,
        /// <summary>
        /// Top Left Tow
        /// </summary>
        TopLeft_2 = 1,
        /// <summary>
        /// Right Bottom of Slider
        /// </summary>
        Right_1 = 3,
        /// <summary>
        /// Center Full Screen
        /// </summary>
        CenterFullScreen = 4,
        /// <summary>
        /// Group Center One
        /// </summary>
        GroupCenter_1 = 5,
        /// <summary>
        /// Group Center Tow
        /// </summary>
        GroupCenter_2 = 6,
    }
}
