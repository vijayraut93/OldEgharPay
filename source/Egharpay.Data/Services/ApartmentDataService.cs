using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Data.Interfaces;
using Egharpay.Data.Models;

namespace Egharpay.Data.Services
{
    public class ApartmentDataService : EgharpayDataService, IApartmentDataService
    {
        public ApartmentDataService(IEgharpayDatabaseFactory<EgharpayDatabase> databaseFactory, IGenericDataService<DbContext> genericDataService) : base(databaseFactory, genericDataService)
        {
        }
    }
}
