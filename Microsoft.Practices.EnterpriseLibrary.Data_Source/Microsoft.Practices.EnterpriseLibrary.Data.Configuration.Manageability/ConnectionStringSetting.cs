namespace Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability;
    using System;
    using System.Management.Instrumentation;

    [InstrumentationClass(InstrumentationType.Instance)]
    public class ConnectionStringSetting : NamedConfigurationSetting
    {
        private string connectionString;
        private string providerName;

        internal ConnectionStringSetting(string name, string connectionString, string providerName) : base(name)
        {
            this.connectionString = connectionString;
            this.providerName = providerName;
        }

        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
            internal set
            {
                this.connectionString = value;
            }
        }

        public string ProviderName
        {
            get
            {
                return this.providerName;
            }
            internal set
            {
                this.providerName = value;
            }
        }
    }
}

