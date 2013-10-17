namespace Microsoft.Practices.EnterpriseLibrary.Data.Oracle.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using System;
    using System.Configuration;

    public class OracleConnectionSettings : SerializableConfigurationSection
    {
        private const string oracleConnectionDataCollectionProperty = "";
        public const string SectionName = "oracleConnectionSettings";

        public static OracleConnectionSettings GetSettings(IConfigurationSource configurationSource)
        {
            return (configurationSource.GetSection("oracleConnectionSettings") as OracleConnectionSettings);
        }

        [ConfigurationProperty("", IsRequired=false, IsDefaultCollection=true)]
        public NamedElementCollection<OracleConnectionData> OracleConnectionsData
        {
            get
            {
                return (NamedElementCollection<OracleConnectionData>) base[""];
            }
        }
    }
}

