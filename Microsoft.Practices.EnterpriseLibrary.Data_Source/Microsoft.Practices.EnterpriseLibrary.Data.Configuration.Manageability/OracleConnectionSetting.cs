namespace Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability;
    using System;
    using System.Management.Instrumentation;

    [InstrumentationClass(InstrumentationType.Instance)]
    public class OracleConnectionSetting : NamedConfigurationSetting
    {
        private string[] packages;

        internal OracleConnectionSetting(string name, string[] packages) : base(name)
        {
            this.packages = packages;
        }

        public string[] Packages
        {
            get
            {
                return this.packages;
            }
            internal set
            {
                this.packages = value;
            }
        }
    }
}

