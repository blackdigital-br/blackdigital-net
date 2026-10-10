

namespace BlackDigital.Model
{
    /// <summary>
    /// Provides extension methods for filtering queries by the update date of models implementing <see cref="IUpdated"/>.
    /// </summary>
    public static class UpdatedFilter
    {
        /// <summary>
        /// Finds models updated exactly on the specified date.
        /// </summary>
        /// <typeparam name="TUpdated">The type of the model implementing <see cref="IUpdated"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="updated">The exact update date to match.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TUpdated> FindUpdated<TUpdated>(this IQueryable<TUpdated> query, DateTime updated)
            where TUpdated : IUpdated
            => query.Where(model => model.Updated == updated);


        /// <summary>
        /// Filters the query by an exact update date, if provided.
        /// </summary>
        /// <typeparam name="TUpdated">The type of the model implementing <see cref="IUpdated"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="updated">The exact update date to match, or <c>null</c> to not filter.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TUpdated> FilterUpdated<TUpdated>(this IQueryable<TUpdated> query, DateTime? updated)
            where TUpdated : IUpdated
        {
            if (updated.HasValue)
                return query.Where(model => model.Updated == updated.Value);

            return query;
        }

        /// <summary>
        /// Filters the query to include only models updated on or before the specified maximum date, if provided.
        /// </summary>
        /// <typeparam name="TUpdated">The type of the model implementing <see cref="IUpdated"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="maxUpdated">The maximum update date, or <c>null</c> to not filter.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TUpdated> FilterMaxUpdated<TUpdated>(this IQueryable<TUpdated> query, DateTime? maxUpdated)
            where TUpdated : IUpdated
        {
            if (maxUpdated.HasValue)
                return query.Where(model => model.Updated <= maxUpdated.Value);
            return query;
        }

        /// <summary>
        /// Filters the query to include only models updated on or after the specified minimum date, if provided.
        /// </summary>
        /// <typeparam name="TUpdated">The type of the model implementing <see cref="IUpdated"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="minUpdated">The minimum update date, or <c>null</c> to not filter.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TUpdated> FilterMinUpdated<TUpdated>(this IQueryable<TUpdated> query, DateTime? minUpdated)
            where TUpdated : IUpdated
        {
            if (minUpdated.HasValue)
                return query.Where(model => model.Updated >= minUpdated.Value);
            return query;
        }

        /// <summary>
        /// Filters the query to include only models updated within the specified date range.
        /// </summary>
        /// <typeparam name="TUpdated">The type of the model implementing <see cref="IUpdated"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="minUpdated">The minimum update date, or <c>null</c> to not filter.</param>
        /// <param name="maxUpdated">The maximum update date, or <c>null</c> to not filter.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TUpdated> FilterUpdatedRange<TUpdated>(this IQueryable<TUpdated> query, DateTime? minUpdated, DateTime? maxUpdated)
            where TUpdated : IUpdated
        {
            if (minUpdated.HasValue && maxUpdated.HasValue)
                return query.Where(model => model.Updated >= minUpdated.Value && model.Updated <= maxUpdated.Value);
            return query;
        }
    }
}
