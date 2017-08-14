using System;
using Egharpay.Data.Interfaces;

namespace Egharpay.Data.Models
{
    public class EgharpayDatabaseFactory : IEgharpayDatabaseFactory<EgharpayDatabase>
    {
        public string NameOrConnectionString { get; }

        public EgharpayDatabaseFactory(string nameOrConnectionString)
        {
            NameOrConnectionString = nameOrConnectionString;
        }

        public EgharpayDatabase CreateContext()
        {
            ValidateConnectionString();
            var context = new EgharpayDatabase(NameOrConnectionString);
           // context.UseSerilog();

            return context;
        }

        public EgharpayDatabase Create()
        {
            ValidateConnectionString();
            return new EgharpayDatabase(NameOrConnectionString);
        }

        public EgharpayDatabase Create(int organisationId)
        {
            ValidateConnectionString();
            return new EgharpayDatabase(NameOrConnectionString, organisationId);
        }

        private void ValidateConnectionString()
        {
            if (string.IsNullOrWhiteSpace(NameOrConnectionString))
                throw new NullReferenceException("OmbrosDatabaseFactory expects a valid NameOrConnectionString");
        }
    }
}
