namespace Microsoft.Practices.EnterpriseLibrary.Data.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using System;
    using System.Configuration;

    public class DbProviderMapping : NamedConfigurationElement
    {
        private const string databaseTypeProperty = "databaseType";
        internal const string DefaultGenericProviderName = "generic";
        public const string DefaultOracleProviderName = "System.Data.OracleClient";
        public const string DefaultSqlProviderName = "System.Data.SqlClient";
        private static AssemblyQualifiedTypeNameConverter typeConverter = new AssemblyQualifiedTypeNameConverter();

        public DbProviderMapping()
        {
        }

        public DbProviderMapping(string dbProviderName, string databaseTypeName) : base(dbProviderName)
        {
            this.DatabaseTypeName = databaseTypeName;
        }

        public DbProviderMapping(string dbProviderName, Type databaseType) : this(dbProviderName, (string) typeConverter.ConvertTo(databaseType, typeof(string)))
        {
        }

        public Type DatabaseType
        {
            get
            {
                return (Type) typeConverter.ConvertFrom(this.DatabaseTypeName);
            }
            set
            {
                this.DatabaseTypeName = typeConverter.ConvertToString(value);
            }
        }

        [ConfigurationProperty("databaseType")]
        public string DatabaseTypeName
        {
            get
            {
                return (string) base["databaseType"];
            }
            set
            {
                base["databaseType"] = value;
            }
        }

        public string DbProviderName
        {
            get
            {
                return base.Name;
            }
        }
    }
}

