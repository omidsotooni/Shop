using EndPoint.Site.Utilities;
using Hangfire.Dashboard;

namespace EndPoint.Site.AuthorizationFilter
{
    public class AuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            var userIsAuthenticated = httpContext.User.Identity?.IsAuthenticated ?? false;
            if ((ClaimUtility.GetRolse(httpContext.User).Where(o => o == "Admin").Count() >= 1
                || ClaimUtility.GetRolse(httpContext.User).Where(o => o == "Operator").Count() >= 1)
                && userIsAuthenticated)
                return true;

            return false;
        }

        public bool Authorize2(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return httpContext.User.Identity?.IsAuthenticated ?? false;
        }

        //public bool Authorize3(DashboardContext context)
        //{
        //    // In case you need an OWIN context, use the next line, `OwinContext` class
        //    // is the part of the `Microsoft.Owin` package.
        //    var owinContext = new OwinContext(context.GetOwinEnvironment());

        //    // Allow all authenticated users to see the Dashboard (potentially dangerous).
        //    return owinContext.Authentication.User.Identity.IsAuthenticated;
        //}
    }

}
