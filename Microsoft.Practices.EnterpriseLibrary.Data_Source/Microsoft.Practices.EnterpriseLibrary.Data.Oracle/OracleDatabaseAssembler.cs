namespace Microsoft.Practices.EnterpriseLibrary.Data.Oracle
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Data.Oracle.Configuration;
    using System;
    using System.Configuration;

    internal class OracleDatabaseAssembler : IDatabaseAssembler
    {
        public Database Assemble(string name, ConnectionStringSettings connectionStringSettings, IConfigurationSource configurationSource)
        {
            OracleConnectionSettings settings = OracleConnectionSettings.GetSettings(configurationSource);
            if (settings != null)
            {
                OracleConnectionData data = settings.OracleConnectionsData.Get(name);
                if (data != null)
                {
                    IOraclePackage[] packages = new IOraclePackage[data.Packages.Count];
                    int num = 0;
                    foreach (IOraclePackage package in data.Packages)
                    {
                        packages[num++] = package;
                    }
                    return new OracleDatabase(connectionStringSettings.ConnectionString, packages);
                }
            }
            return new OracleDatabase(connectionStringSettings.ConnectionString);
        }
    }
}

