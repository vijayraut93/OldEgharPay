using System.Data.Entity;
using HR.Entity;
using Egharpay.Data.Models;

namespace Egharpay.Data.Models
{
    /// Ensure the generated EgharpayDatabase also references OrganisationDbContext
    /// and the OnModelCreating has the following as its last line of code:  base.OnModelCreating(modelBuilder);
    public partial class EgharpayDatabase : OrganisationDbContext
    {
        public EgharpayDatabase(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Initialise();
        }

        public EgharpayDatabase(string nameOrConnectionString, int organisationId) : base(nameOrConnectionString, organisationId)
        {
            Initialise();
        }

        private void Initialise()
        {
            //Disable initializer
            Database.SetInitializer<EgharpayDatabase>(null);
            Database.CommandTimeout = 300;
            Configuration.ProxyCreationEnabled = false;
        }

        // Ensure this function is called with in the generated EgharpayDatabase
        
        protected void PersonnelModelCreating(DbModelBuilder modelBuilder)
        {            
          
        }

    }
}
