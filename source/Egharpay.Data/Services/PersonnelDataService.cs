using System.Data.Entity;
using Egharpay.Data.Interfaces;
using Egharpay.Data.Models;

namespace Egharpay.Data.Services
{
    public class PersonnelTestDataService : EgharpayDataService, IPersonnelTestDataService
    {
        public PersonnelTestDataService(IEgharpayDatabaseFactory<EgharpayDatabase> databaseFactory, IGenericDataService<DbContext> genericDataService) : base(databaseFactory, genericDataService)
        {
        }
    }
}
