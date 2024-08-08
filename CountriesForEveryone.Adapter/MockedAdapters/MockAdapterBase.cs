using CountriesForEveryone.Adapter.Extensions;
using CountriesForEveryone.Core.Adapters;
using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Adapter.MockedAdapters
{
    public abstract class MockAdapterBase<TEntity, TEntityCriteria> : IQueryAdapter<TEntity, TEntityCriteria>
    {
        private readonly Lazy<List<TEntity>> _entities;

        protected MockAdapterBase()
        {
            _entities = new Lazy<List<TEntity>>(LoadMockedEntities);
        }

        protected List<TEntity> Entities => _entities.Value;

        public virtual Task<TEntity?> Get()
        {
            var entity = Entities.FirstOrDefault();
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<TEntity>> GetAll()
        {
            return Task.FromResult<IEnumerable<TEntity>>(Entities);
        }

        public Task<PagedList<TEntity>> GetFiltered(FilterCriteria<TEntityCriteria> criteria)
        {
            var filteredEntities = Filter(criteria, Entities);
            var orderedEntities = Order(criteria, filteredEntities);
            var pagedEntities = orderedEntities.Paginate(criteria.PageSize, criteria.PageNumber);

            var result = new PagedList<TEntity>()
            {
                Items = pagedEntities.ToList(),
                TotalItemCount = filteredEntities.Count()
            };

            return Task.FromResult(result);
        }

        protected abstract List<TEntity> LoadMockedEntities();
        protected abstract IEnumerable<TEntity> Filter(FilterCriteria<TEntityCriteria> filterCriteria, IEnumerable<TEntity> entities);
        protected abstract IEnumerable<TEntity> Order(FilterCriteria<TEntityCriteria> filterCriteria, IEnumerable<TEntity> filteredEntities);
    }
}
