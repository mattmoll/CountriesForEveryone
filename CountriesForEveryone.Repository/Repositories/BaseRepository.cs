using System.Transactions;

namespace CountriesForEveryone.Repository.Repositories;

public abstract class BaseRepository
{
    protected static TransactionScope CreateTransaction()
    {
        return new TransactionScope(TransactionScopeOption.Required,
            new TransactionOptions()
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            },
            TransactionScopeAsyncFlowOption.Enabled);
    }
}