namespace Microsoft.Practices.EnterpriseLibrary.Data.Oracle.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
    using System;
    using System.Configuration;

    public class OraclePackageData : NamedConfigurationElement, IOraclePackage
    {
        private const string prefixProperty = "prefix";

        public OraclePackageData()
        {
            this.Prefix = string.Empty;
        }

        public OraclePackageData(string name, string prefix) : base(name)
        {
            this.Prefix = prefix;
        }

        [ConfigurationProperty("prefix", IsRequired=true)]
        public string Prefix
        {
            get
            {
                return (string) base["prefix"];
            }
            set
            {
                base["prefix"] = value;
            }
        }
    }
}

