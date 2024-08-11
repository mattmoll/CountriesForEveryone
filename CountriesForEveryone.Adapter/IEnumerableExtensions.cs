using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Adapter.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> enumerable, int? pageSize, int? pageNumber)
        {
            if (!pageSize.HasValue)
            {
                pageSize = 10;
            }

            if (!pageNumber.HasValue)
            {
                pageNumber = 1;
            }

            var skipCount = pageSize.Value * (pageNumber.Value - 1);

            return enumerable
                .Skip(skipCount)
                .Take(pageSize.Value)
                .ToList();
        }

        public static IOrderedEnumerable<T> OrderEntitiesBy<T, U>(this IEnumerable<T> entities, Func<T, U> keyDescriptor, OrderDirection orderDirection)
        {
            return orderDirection == OrderDirection.Ascending
                ? entities.OrderBy(keyDescriptor)
                : entities.OrderByDescending(keyDescriptor);
        }
    }
}
