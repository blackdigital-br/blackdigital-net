
namespace BlackDigital.Model
{
    public static class DeletedFilter
    {
        public static IQueryable<TDeleted> HasDeleted<TDeleted>(this IQueryable<TDeleted> query)
            where TDeleted : IDeleted
                => query.Where(model => model.Deleted != null);

        public static IQueryable<TDeleted> HasNotDeleted<TDeleted>(this IQueryable<TDeleted> query)
            where TDeleted : IDeleted
                => query.Where(model => model.Deleted == null);

        public static IQueryable<TDeleted> FindDeleted<TDeleted>(this IQueryable<TDeleted> query, DateTime deleted)
            where TDeleted : IDeleted
            => query.Where(model => model.Deleted == deleted);

        public static IQueryable<TDeleted> FilterDeleted<TDeleted>(this IQueryable<TDeleted> query, DateTime? deleted)
            where TDeleted : IDeleted
        {
            if (deleted.HasValue)
                return query.Where(model => model.Deleted == deleted.Value);

            return query;
        }

        public static IQueryable<TDeleted> FilterMaxDeleted<TDeleted>(this IQueryable<TDeleted> query, DateTime? maxDeleted)
            where TDeleted : IDeleted
        {
            if (maxDeleted.HasValue)
                return query.Where(model => model.Deleted <= maxDeleted.Value);
            
            return query;
        }

        public static IQueryable<TDeleted> FilterMinDeleted<TDeleted>(this IQueryable<TDeleted> query, DateTime? minDeleted)
            where TDeleted : IDeleted
        {
            if (minDeleted.HasValue)
                return query.Where(model => model.Deleted >= minDeleted.Value);
            
            return query;
        }

        public static IQueryable<TDeleted> FilterDeletedRange<TDeleted>(this IQueryable<TDeleted> query, DateTime? minDeleted, DateTime? maxDeleted)
            where TDeleted : IDeleted
        {
            if (minDeleted.HasValue && maxDeleted.HasValue)
                return query.Where(model => model.Deleted >= minDeleted.Value && model.Deleted <= maxDeleted.Value);
            return query;
        }
    }
}
