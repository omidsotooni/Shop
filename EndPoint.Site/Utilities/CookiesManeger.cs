namespace EndPoint.Site.Utilities
{
    public class CookiesManeger
    {
        /// <summary>
        /// Add Cookie
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        /// <param name="value"></param>
        public void Add(HttpContext context, string token, string value)
        {
            context.Response.Cookies.Append(token, value, GetCookieOptions(context));
        }
        /// <summary>
        /// Contains Cookie
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool Contains(HttpContext context, string token)
        {
            return context.Request.Cookies.ContainsKey(token);
        }
        /// <summary>
        /// Get Values of Cookie
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string GetValue(HttpContext context, string token)
        {
            string cookieValue = string.Empty;
            if (!context.Request.Cookies.TryGetValue(token, out cookieValue))
            {
                return null;
            }
            return cookieValue;
        }
        /// <summary>
        /// Remove Cookie
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        public void Remove(HttpContext context, string token)
        {
            if (context.Request.Cookies.ContainsKey(token))
            {
                context.Response.Cookies.Delete(token);
            }
        }
        /// <summary>
        /// Get Browser Id as GUID
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Guid GetBrowserId(HttpContext context)
        {
            string browserId = GetValue(context, "BowserId");
            if (browserId == null)
            {
                string value = Guid.NewGuid().ToString();
                Add(context, "BowserId", value);
                browserId = value;
            }
            Guid guidBowser;
            Guid.TryParse(browserId, out guidBowser);
            return guidBowser;
        }
        /// <summary>
        /// Get Cookie Options
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private CookieOptions GetCookieOptions(HttpContext context)
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Path = context.Request.PathBase.HasValue ? context.Request.PathBase.ToString() : "/",
                Secure = context.Request.IsHttps,
                Expires = DateTime.Now.AddDays(31),
            };
        }
    }
}