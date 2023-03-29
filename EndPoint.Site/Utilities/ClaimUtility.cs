using Shop.Common;
using System.Security.Claims;

namespace EndPoint.Site.Utilities
{
    public class ClaimUtility
    {
        public static long? GetUserId(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                if (claimsIdentity == null)
                {
                    return null;
                }
                var _claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                if (_claim == null)
                {
                    return null;
                }
                long userId = long.Parse(_claim.Value);
                return userId;
            }
            catch (Exception ex)
            {
                Utility.ExceptionMessage(ex);
                return null;
            }
        }
    }
}
