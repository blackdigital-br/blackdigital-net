﻿namespace BlackDigital.Model
{
    public static class ActiveFilter
    {
        public static IQueryable<TActive> IsActive<TActive>(this IQueryable<TActive> query)
            where TActive : IActive
            => query.FilterActive(true);

        public static IQueryable<TActive> NotIsActive<TActive>(this IQueryable<TActive> query)
            where TActive : IActive
            => query.FilterActive(false);

        public static IQueryable<TActive> FilterActive<TActive>(this IQueryable<TActive> query, bool? active)
            where TActive : IActive
        {
            if (active.HasValue)
                return query.Where(model => model.Active == active.Value);

            return query;
        }
    }
}
