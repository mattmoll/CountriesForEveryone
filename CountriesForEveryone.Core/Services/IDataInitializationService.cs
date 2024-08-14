namespace CountriesForEveryone.Core.Services
{
    public interface IDataInitializationService
    {
        Task LoadCountriesData(bool forceDataFetchingUpdate);
    }
}
