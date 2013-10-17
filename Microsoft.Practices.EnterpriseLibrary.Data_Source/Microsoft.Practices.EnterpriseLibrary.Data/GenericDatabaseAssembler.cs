namespace Microsoft.Practices.EnterpriseLibrary.Data
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
    using System;
    using System.Configuration;
    using System.Data.Common;

    internal class GenericDatabaseAssembler : IDatabaseAssembler
    {
        public Database Assemble(string name, ConnectionStringSettings connectionStringSettings, IConfigurationSource configurationSource)
        {
            return new GenericDatabase(connectionStringSettings.ConnectionString, DbProviderFactories.GetFactory(connectionStringSettings.ProviderName));
        }
    }
}

