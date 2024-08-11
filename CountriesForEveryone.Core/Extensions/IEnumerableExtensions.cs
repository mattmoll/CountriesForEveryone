using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static int MAX_PAGE_SIZE = 200;
        public static int DEFAULT_PAGE_SIZE = 20;

        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> enumerable, int? pageSize, int? pageNumber)
        {
            if (!pageSize.HasValue || pageSize.Value <= 0)
            {
                pageSize = DEFAULT_PAGE_SIZE;
            }

            if (pageSize > MAX_PAGE_SIZE)
            {
                pageSize = MAX_PAGE_SIZE;
            }

            if (!pageNumber.HasValue || pageNumber <= 0)
            {
                pageNumber = 1;
            }

            var skipCount = pageSize.Value * (pageNumber.Value - 1);

            return enumerable
                .Skip(skipCount)
                .Take(pageSize.Value);
        }

        public static IOrderedEnumerable<T> OrderEntitiesBy<T, U>(this IEnumerable<T> entities, Func<T, U> keyDescriptor, OrderDirection? orderDirection)
        {
            return !orderDirection.HasValue  || orderDirection.Value == OrderDirection.Ascending
                ? entities.OrderBy(keyDescriptor)
                : entities.OrderByDescending(keyDescriptor);
        }

        public static IOrderedEnumerable<T> ThenOrderEntitiesBy<T, U>(this IOrderedEnumerable<T> entities, Func<T, U> keyDescriptor, OrderDirection orderDirection)
        {
            return orderDirection == OrderDirection.Ascending
                ? entities.ThenBy(keyDescriptor)
                : entities.ThenByDescending(keyDescriptor);
        }
    }
}
