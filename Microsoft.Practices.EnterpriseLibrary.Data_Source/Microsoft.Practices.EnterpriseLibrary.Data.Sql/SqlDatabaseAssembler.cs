namespace Microsoft.Practices.EnterpriseLibrary.Data.Sql
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
    using System;
    using System.Configuration;

    public class SqlDatabaseAssembler : IDatabaseAssembler
    {
        public Database Assemble(string name, ConnectionStringSettings connectionStringSettings, IConfigurationSource configurationSource)
        {
            return new SqlDatabase(connectionStringSettings.ConnectionString);
        }
    }
}

