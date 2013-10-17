namespace Microsoft.Practices.EnterpriseLibrary.Data
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Data.Properties;
    using System;
    using System.Configuration;
    using System.Data.Common;
    using System.Data.OracleClient;
    using System.Data.SqlClient;

    public class DatabaseConfigurationView
    {
        private IConfigurationSource configurationSource;
        private static readonly DbProviderMapping defaultGenericMapping = new DbProviderMapping("generic", typeof(GenericDatabase));
        private static readonly DbProviderMapping defaultOracleMapping = new DbProviderMapping("System.Data.OracleClient", typeof(OracleDatabase));
        private static readonly DbProviderMapping defaultSqlMapping = new DbProviderMapping("System.Data.SqlClient", typeof(SqlDatabase));

        public DatabaseConfigurationView(IConfigurationSource configurationSource)
        {
            this.configurationSource = configurationSource;
        }

        public ConnectionStringSettings GetConnectionStringSettings(string name)
        {
            ConnectionStringSettings settings;
            this.ValidateInstanceName(name);
            ConfigurationSection section = this.configurationSource.GetSection("connectionStrings");
            if ((section != null) && (section is ConnectionStringsSection))
            {
                ConnectionStringsSection section2 = section as ConnectionStringsSection;
                settings = section2.ConnectionStrings[name];
            }
            else
            {
                settings = ConfigurationManager.ConnectionStrings[name];
            }
            ValidateConnectionStringSettings(name, settings);
            return settings;
        }

        private DbProviderMapping GetDefaultMapping(string name, string dbProviderName)
        {
            if ("System.Data.SqlClient".Equals(dbProviderName))
            {
                return defaultSqlMapping;
            }
            if ("System.Data.OracleClient".Equals(dbProviderName))
            {
                return defaultOracleMapping;
            }
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(dbProviderName);
            ValidateDbProviderFactory(name, providerFactory);
            if (SqlClientFactory.Instance == providerFactory)
            {
                return defaultSqlMapping;
            }
            if (OracleClientFactory.Instance == providerFactory)
            {
                return defaultOracleMapping;
            }
            return null;
        }

        private DbProviderMapping GetGenericMapping()
        {
            return defaultGenericMapping;
        }

        public DbProviderMapping GetProviderMapping(string name, string dbProviderName)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Configuration.DatabaseSettings databaseSettings = this.DatabaseSettings;
            if (databaseSettings != null)
            {
                DbProviderMapping mapping = databaseSettings.ProviderMappings.Get(dbProviderName);
                if (mapping != null)
                {
                    return mapping;
                }
            }
            DbProviderMapping defaultMapping = this.GetDefaultMapping(name, dbProviderName);
            if (defaultMapping != null)
            {
                return defaultMapping;
            }
            return this.GetGenericMapping();
        }

        private static void ValidateConnectionStringSettings(string name, ConnectionStringSettings connectionStringSettings)
        {
            if (connectionStringSettings == null)
            {
                throw new ConfigurationErrorsException(string.Format(Resources.Culture, Resources.ExceptionNoDatabaseDefined, new object[] { name }));
            }
            if (string.IsNullOrEmpty(connectionStringSettings.ProviderName))
            {
                throw new ConfigurationErrorsException(string.Format(Resources.Culture, Resources.ExceptionNoProviderDefinedForConnectionString, new object[] { name }));
            }
        }

        private static void ValidateDbProviderFactory(string name, DbProviderFactory providerFactory)
        {
            if (providerFactory == null)
            {
                throw new ConfigurationErrorsException(string.Format(Resources.Culture, Resources.ExceptionNoProviderDefinedForConnectionString, new object[] { name }));
            }
        }

        private void ValidateInstanceName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString);
            }
        }

        public Microsoft.Practices.EnterpriseLibrary.Data.Configuration.DatabaseSettings DatabaseSettings
        {
            get
            {
                return (Microsoft.Practices.EnterpriseLibrary.Data.Configuration.DatabaseSettings) this.configurationSource.GetSection("dataConfiguration");
            }
        }

        public string DefaultName
        {
            get
            {
                return this.DatabaseSettings.DefaultDatabase;
            }
        }
    }
}

