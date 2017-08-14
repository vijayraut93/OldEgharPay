using Egharpay.Data.Models;


namespace Egharpay.Data.Interfaces
{
    public interface IEgharpayDatabaseFactory<T>
    {
        EgharpayDatabase Create();
       // EgharpayDatabase Create(int organisationId);
        T CreateContext();
    }
}
