
namespace BlackDigital.Model
{
    public static class CreatedFilter
    {
        public static IQueryable<TCreated> FindCreated<TCreated>(this IQueryable<TCreated> query, DateTime created)
            where TCreated : ICreated
            => query.Where(model => model.Created == created);

        public static IQueryable<TCreated> FilterCreated<TCreated>(this IQueryable<TCreated> query, DateTime? created)
            where TCreated : ICreated
        {
            if (created.HasValue)
                return query.Where(model => model.Created == created.Value);

            return query;
        }

        public static IQueryable<TCreated> FilterMaxCreated<TCreated>(this IQueryable<TCreated> query, DateTime? maxCreated)
            where TCreated : ICreated
        {
            if (maxCreated.HasValue)
                return query.Where(model => model.Created <= maxCreated.Value);

            return query;
        }

        public static IQueryable<TCreated> FilterMinCreated<TCreated>(this IQueryable<TCreated> query, DateTime? minCreated)
            where TCreated : ICreated
        {
            if (minCreated.HasValue)
                return query.Where(model => model.Created >= minCreated.Value);
            return query;
        }

        public static IQueryable<TCreated> FilterCreatedRange<TCreated>(this IQueryable<TCreated> query, DateTime? minCreated, DateTime? maxCreated)
            where TCreated : ICreated
        {
            if (minCreated.HasValue && maxCreated.HasValue)
                return query.Where(model => model.Created >= minCreated.Value && model.Created <= maxCreated.Value);

            return query;
        }
    }
}
