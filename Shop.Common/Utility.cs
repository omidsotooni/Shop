namespace Shop.Common
{
    public static class Utility
    {
        #region Methods
        /// <summary>
        /// Pagination
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowsCount"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> ToPaged<TSource>(this IEnumerable<TSource> source, int page, int pageSize, out int rowsCount)
        {
            rowsCount = source.Count();
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        #endregion
    }
}
