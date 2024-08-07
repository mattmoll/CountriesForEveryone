using AutoMapper;

namespace CountriesForEveryone.Adapter.Adapters
{
    public class BaseAdapter
    {
        public BaseAdapter(HttpClient httpClient, IMapper mapper)
        {
            HttpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            Mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }
        protected HttpClient HttpClient { get; }
        protected IMapper Mapper { get; }
    }

    public class BaseAdapter<TEntity> : BaseAdapter
    {
        public BaseAdapter(HttpClient httpClient, IMapper mapper) : base(httpClient, mapper)
        {
        }
    }
}
