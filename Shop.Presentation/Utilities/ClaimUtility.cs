using Shop.Common;
using System.Security.Claims;

namespace EndPoint.Site.Utilities
{
    public class ClaimUtility
    {
        /// <summary>
        /// Get User ID
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Get User email
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static string GetUserEmail(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                return claimsIdentity.FindFirst(ClaimTypes.Email).Value;
            }
            catch (Exception ex)
            {
                Utility.ExceptionMessage(ex);
                return null;
            }
        }
        /// <summary>
        /// Get roles
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static List<string> GetRolse(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                List<string> rolse = new List<string>();
                foreach (var item in claimsIdentity.Claims.Where(p => p.Type.EndsWith("role")))
                {
                    rolse.Add(item.Value);
                }
                return rolse;
            }
            catch (Exception ex)
            {
                Utility.ExceptionMessage(ex);
                return null;
            }
        }
    }
}
