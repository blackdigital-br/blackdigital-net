
namespace BlackDigital.Model
{
    /// <summary>
    /// Provides extension methods for filtering queries by the deletion date of models implementing <see cref="IDeleted"/>.
    /// </summary>
    public static class DeletedFilter
    {
        /// <summary>
        /// Filters the query to include only soft-deleted models (with a non-null deletion date).
        /// </summary>
        /// <typeparam name="TDeleted">The type of the model implementing <see cref="IDeleted"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TDeleted> HasDeleted<TDeleted>(this IQueryable<TDeleted> query)
            where TDeleted : IDeleted
                => query.Where(model => model.Deleted != null);

        /// <summary>
        /// Filters the query to include only non-deleted models (with a null deletion date).
        /// </summary>
        /// <typeparam name="TDeleted">The type of the model implementing <see cref="IDeleted"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TDeleted> HasNotDeleted<TDeleted>(this IQueryable<TDeleted> query)
            where TDeleted : IDeleted
                => query.Where(model => model.Deleted == null);

        /// <summary>
        /// Finds models deleted exactly on the specified date.
        /// </summary>
        /// <typeparam name="TDeleted">The type of the model implementing <see cref="IDeleted"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="deleted">The exact deletion date to match.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TDeleted> FindDeleted<TDeleted>(this IQueryable<TDeleted> query, DateTime deleted)
            where TDeleted : IDeleted
            => query.Where(model => model.Deleted == deleted);

        /// <summary>
        /// Filters the query by an exact deletion date, if provided.
        /// </summary>
        /// <typeparam name="TDeleted">The type of the model implementing <see cref="IDeleted"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="deleted">The exact deletion date to match, or <c>null</c> to not filter.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TDeleted> FilterDeleted<TDeleted>(this IQueryable<TDeleted> query, DateTime? deleted)
            where TDeleted : IDeleted
        {
            if (deleted.HasValue)
                return query.Where(model => model.Deleted == deleted.Value);

            return query;
        }

        /// <summary>
        /// Filters the query to include only models deleted on or before the specified maximum date, if provided.
        /// </summary>
        /// <typeparam name="TDeleted">The type of the model implementing <see cref="IDeleted"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="maxDeleted">The maximum deletion date, or <c>null</c> to not filter.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TDeleted> FilterMaxDeleted<TDeleted>(this IQueryable<TDeleted> query, DateTime? maxDeleted)
            where TDeleted : IDeleted
        {
            if (maxDeleted.HasValue)
                return query.Where(model => model.Deleted <= maxDeleted.Value);

            return query;
        }

        /// <summary>
        /// Filters the query to include only models deleted on or after the specified minimum date, if provided.
        /// </summary>
        /// <typeparam name="TDeleted">The type of the model implementing <see cref="IDeleted"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="minDeleted">The minimum deletion date, or <c>null</c> to not filter.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TDeleted> FilterMinDeleted<TDeleted>(this IQueryable<TDeleted> query, DateTime? minDeleted)
            where TDeleted : IDeleted
        {
            if (minDeleted.HasValue)
                return query.Where(model => model.Deleted >= minDeleted.Value);

            return query;
        }

        /// <summary>
        /// Filters the query to include only models deleted within the specified date range.
        /// </summary>
        /// <typeparam name="TDeleted">The type of the model implementing <see cref="IDeleted"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="minDeleted">The minimum deletion date, or <c>null</c> to not filter.</param>
        /// <param name="maxDeleted">The maximum deletion date, or <c>null</c> to not filter.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TDeleted> FilterDeletedRange<TDeleted>(this IQueryable<TDeleted> query, DateTime? minDeleted, DateTime? maxDeleted)
            where TDeleted : IDeleted
        {
            if (minDeleted.HasValue && maxDeleted.HasValue)
                return query.Where(model => model.Deleted >= minDeleted.Value && model.Deleted <= maxDeleted.Value);
            return query;
        }
    }
}
