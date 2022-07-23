using ShopWithASP.NETCore.Doima.Entities.Commons;

namespace ShopWithASP.NETCore.Doima.Entities.Users
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<UserInRole> UserInRoles { get; set; }
    }
}
