namespace BlackDigital.Model
{
    /// <summary>
    /// Provides extension methods for filtering queries by the active state of models implementing <see cref="IActive"/>.
    /// </summary>
    public static class ActiveFilter
    {
        /// <summary>
        /// Filters the query to include only active models.
        /// </summary>
        /// <typeparam name="TActive">The type of the model implementing <see cref="IActive"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TActive> IsActive<TActive>(this IQueryable<TActive> query)
            where TActive : IActive
            => query.FilterActive(true);

        /// <summary>
        /// Filters the query to include only inactive models.
        /// </summary>
        /// <typeparam name="TActive">The type of the model implementing <see cref="IActive"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TActive> NotIsActive<TActive>(this IQueryable<TActive> query)
            where TActive : IActive
            => query.FilterActive(false);

        /// <summary>
        /// Filters the query by the active state, if provided.
        /// </summary>
        /// <typeparam name="TActive">The type of the model implementing <see cref="IActive"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="active">The active state to filter by, or <c>null</c> to not filter.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TActive> FilterActive<TActive>(this IQueryable<TActive> query, bool? active)
            where TActive : IActive
        {
            if (active.HasValue)
                return query.Where(model => model.Active == active.Value);

            return query;
        }
    }
}
