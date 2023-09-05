namespace BlackDigital.Model
{
    public static class IdFilter
    {
        public static IQueryable<TId> FindId<TId>(this IQueryable<TId> query, Id id)
            where TId : IId
            => query.Where(model => model.Id == id);

        public static IQueryable<TId> FilterId<TId>(this IQueryable<TId> query, Id? id)
            where TId : IId
        {
            if (id.HasValue)
                return query.Where(model => model.Id == id.Value);

            return query;
        }

        public static IQueryable<TId> FindId<TId, TKey>(this IQueryable<TId> query, TKey id)
            where TId : IId<TKey>
            where TKey : struct
        {
            return ExpressionHelper.Filter(query, item => item.Id, id);
        }

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
