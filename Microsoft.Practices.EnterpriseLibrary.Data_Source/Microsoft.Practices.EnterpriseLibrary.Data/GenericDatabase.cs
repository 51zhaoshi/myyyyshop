namespace Microsoft.Practices.EnterpriseLibrary.Data
{
    using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Data.Properties;
    using System;
    using System.Data.Common;

    [DatabaseAssembler(typeof(GenericDatabaseAssembler))]
    public class GenericDatabase : Database
    {
        public GenericDatabase(string connectionString, DbProviderFactory dbProviderFactory) : base(connectionString, dbProviderFactory)
        {
        }

        protected override void DeriveParameters(DbCommand discoveryCommand)
        {
            throw new NotSupportedException(Resources.ExceptionParameterDiscoveryNotSupportedOnGenericDatabase);
        }
    }
}

