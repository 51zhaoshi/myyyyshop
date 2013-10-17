namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using System;
    using System.Management.Instrumentation;

    [InstrumentationClass(InstrumentationType.Abstract)]
    public abstract class ConfigurationSetting
    {
        private string applicationName;
        private string sectionName;

        protected ConfigurationSetting()
        {
        }

        public string ApplicationName
        {
            get
            {
                return this.applicationName;
            }
            internal set
            {
                this.applicationName = value;
            }
        }

        public string SectionName
        {
            get
            {
                return this.sectionName;
            }
            internal set
            {
                this.sectionName = value;
            }
        }
    }
}

