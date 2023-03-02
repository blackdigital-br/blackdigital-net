
namespace BlackDigital
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
    }
}
