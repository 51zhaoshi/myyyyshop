namespace Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability;
    using System;
    using System.Management.Instrumentation;

    [InstrumentationClass(InstrumentationType.Instance)]
    public class ProviderMappingSetting : NamedConfigurationSetting
    {
        private string databaseTypeName;

        internal ProviderMappingSetting(string name, string databaseTypeName) : base(name)
        {
            this.databaseTypeName = databaseTypeName;
        }

        public string DatabaseType
        {
            get
            {
                return this.databaseTypeName;
            }
            internal set
            {
                this.databaseTypeName = value;
            }
        }
    }
}

