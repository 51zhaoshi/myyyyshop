namespace Microsoft.Practices.EnterpriseLibrary.Data.Oracle.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using System;
    using System.Configuration;

    public class OracleConnectionData : NamedConfigurationElement
    {
        private const string packagesProperty = "packages";

        [ConfigurationProperty("packages", IsRequired=true)]
        public NamedElementCollection<OraclePackageData> Packages
        {
            get
            {
                return (NamedElementCollection<OraclePackageData>) base["packages"];
            }
        }
    }
}

