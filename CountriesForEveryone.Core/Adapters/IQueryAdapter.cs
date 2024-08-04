using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Core.Adapters
{
    public interface IQueryAdapter<TEntity, TFilterCriteria>
    {
        Task<TEntity> Get(Guid id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<PagedList<TEntity>> GetFiltered(FilterCriteria<TFilterCriteria> criteria);
    }
}