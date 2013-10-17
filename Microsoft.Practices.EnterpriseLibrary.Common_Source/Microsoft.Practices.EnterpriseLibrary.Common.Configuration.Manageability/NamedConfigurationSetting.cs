namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using System;
    using System.Management.Instrumentation;

    [InstrumentationClass(InstrumentationType.Abstract)]
    public abstract class NamedConfigurationSetting : ConfigurationSetting
    {
        private string name;

        protected NamedConfigurationSetting(string name)
        {
            this.Name = name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            internal set
            {
                this.name = value;
            }
        }
    }
}

