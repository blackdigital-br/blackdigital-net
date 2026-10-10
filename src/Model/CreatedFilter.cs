
namespace BlackDigital.Model
{
    /// <summary>
    /// Provides extension methods for filtering queries by the creation date of models implementing <see cref="ICreated"/>.
    /// </summary>
    public static class CreatedFilter
    {
        /// <summary>
        /// Finds models created exactly on the specified date.
        /// </summary>
        /// <typeparam name="TCreated">The type of the model implementing <see cref="ICreated"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="created">The exact creation date to match.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TCreated> FindCreated<TCreated>(this IQueryable<TCreated> query, DateTime created)
            where TCreated : ICreated
            => query.Where(model => model.Created == created);

        /// <summary>
        /// Filters the query by an exact creation date, if provided.
        /// </summary>
        /// <typeparam name="TCreated">The type of the model implementing <see cref="ICreated"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="created">The exact creation date to match, or <c>null</c> to not filter.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TCreated> FilterCreated<TCreated>(this IQueryable<TCreated> query, DateTime? created)
            where TCreated : ICreated
        {
            if (created.HasValue)
                return query.Where(model => model.Created == created.Value);

            return query;
        }

        /// <summary>
        /// Filters the query to include only models created on or before the specified maximum date, if provided.
        /// </summary>
        /// <typeparam name="TCreated">The type of the model implementing <see cref="ICreated"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="maxCreated">The maximum creation date, or <c>null</c> to not filter.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TCreated> FilterMaxCreated<TCreated>(this IQueryable<TCreated> query, DateTime? maxCreated)
            where TCreated : ICreated
        {
            if (maxCreated.HasValue)
                return query.Where(model => model.Created <= maxCreated.Value);

            return query;
        }

        /// <summary>
        /// Filters the query to include only models created on or after the specified minimum date, if provided.
        /// </summary>
        /// <typeparam name="TCreated">The type of the model implementing <see cref="ICreated"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="minCreated">The minimum creation date, or <c>null</c> to not filter.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TCreated> FilterMinCreated<TCreated>(this IQueryable<TCreated> query, DateTime? minCreated)
            where TCreated : ICreated
        {
            if (minCreated.HasValue)
                return query.Where(model => model.Created >= minCreated.Value);
            return query;
        }

        /// <summary>
        /// Filters the query to include only models created within the specified date range.
        /// </summary>
        /// <typeparam name="TCreated">The type of the model implementing <see cref="ICreated"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="minCreated">The minimum creation date, or <c>null</c> to not filter.</param>
        /// <param name="maxCreated">The maximum creation date, or <c>null</c> to not filter.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TCreated> FilterCreatedRange<TCreated>(this IQueryable<TCreated> query, DateTime? minCreated, DateTime? maxCreated)
            where TCreated : ICreated
        {
            if (minCreated.HasValue && maxCreated.HasValue)
                return query.Where(model => model.Created >= minCreated.Value && model.Created <= maxCreated.Value);

            return query;
        }
    }
}
