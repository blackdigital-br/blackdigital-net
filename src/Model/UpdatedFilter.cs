

namespace BlackDigital.Model
{
    public static class UpdatedFilter
    {
        public static IQueryable<TUpdated> FindUpdated<TUpdated>(this IQueryable<TUpdated> query, DateTime updated)
            where TUpdated : IUpdated
            => query.Where(model => model.Updated == updated);


        public static IQueryable<TUpdated> FilterUpdated<TUpdated>(this IQueryable<TUpdated> query, DateTime? updated)
            where TUpdated : IUpdated
        {
            if (updated.HasValue)
                return query.Where(model => model.Updated == updated.Value);

            return query;
        }

        public static IQueryable<TUpdated> FilterMaxUpdated<TUpdated>(this IQueryable<TUpdated> query, DateTime? maxUpdated)
            where TUpdated : IUpdated
        {
            if (maxUpdated.HasValue)
                return query.Where(model => model.Updated <= maxUpdated.Value);
            return query;
        }

        public static IQueryable<TUpdated> FilterMinUpdated<TUpdated>(this IQueryable<TUpdated> query, DateTime? minUpdated)
            where TUpdated : IUpdated
        {
            if (minUpdated.HasValue)
                return query.Where(model => model.Updated >= minUpdated.Value);
            return query;
        }

        public static IQueryable<TUpdated> FilterUpdatedRange<TUpdated>(this IQueryable<TUpdated> query, DateTime? minUpdated, DateTime? maxUpdated)
            where TUpdated : IUpdated
        {
            if (minUpdated.HasValue && maxUpdated.HasValue)
                return query.Where(model => model.Updated >= minUpdated.Value && model.Updated <= maxUpdated.Value);
            return query;
        }
    }
}
