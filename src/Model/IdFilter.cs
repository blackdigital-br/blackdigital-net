namespace BlackDigital.Model
{
    /// <summary>
    /// Provides extension methods for filtering queries by the identifier of models implementing <see cref="IId"/> or <see cref="IId{TKey}"/>.
    /// </summary>
    public static class IdFilter
    {
        /// <summary>
        /// Finds a model with the specified <see cref="Id"/>.
        /// </summary>
        /// <typeparam name="TId">The type of the model implementing <see cref="IId"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="id">The identifier to find.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TId> FindId<TId>(this IQueryable<TId> query, Id id)
            where TId : IId
            => query.Where(model => model.Id == id);

        /// <summary>
        /// Filters the query by the specified <see cref="Id"/>, if provided.
        /// </summary>
        /// <typeparam name="TId">The type of the model implementing <see cref="IId"/>.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="id">The identifier to filter by, or <c>null</c> to not filter.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TId> FilterId<TId>(this IQueryable<TId> query, Id? id)
            where TId : IId
        {
            if (id.HasValue)
                return query.Where(model => model.Id == id.Value);

            return query;
        }

        /// <summary>
        /// Finds a model with the specified typed identifier.
        /// </summary>
        /// <typeparam name="TId">The type of the model implementing <see cref="IId{TKey}"/>.</typeparam>
        /// <typeparam name="TKey">The type of the identifier.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="id">The identifier to find.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TId> FindId<TId, TKey>(this IQueryable<TId> query, TKey id)
            where TId : IId<TKey>
            where TKey : struct
        {
            return ExpressionHelper.Filter(query, item => item.Id, id);
        }

        /// <summary>
        /// Filters the query by the specified typed identifier, if provided.
        /// </summary>
        /// <typeparam name="TId">The type of the model implementing <see cref="IId{TKey}"/>.</typeparam>
        /// <typeparam name="TKey">The type of the identifier.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="id">The identifier to filter by, or <c>null</c> to not filter.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<TId> FilterId<TId, TKey>(this IQueryable<TId> query, TKey? id)
            where TId : IId<TKey>
            where TKey : struct
        {
            if (id.HasValue)
                return ExpressionHelper.Filter(query, item => item.Id, id.Value);

            return query;
        }
    }
}
