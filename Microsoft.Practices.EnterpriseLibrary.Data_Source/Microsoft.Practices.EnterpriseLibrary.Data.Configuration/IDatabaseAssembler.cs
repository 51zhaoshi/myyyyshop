namespace Microsoft.Practices.EnterpriseLibrary.Data.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Configuration;

    public interface IDatabaseAssembler
    {
        Database Assemble(string name, ConnectionStringSettings connectionStringSettings, IConfigurationSource configurationSource);
    }
}

