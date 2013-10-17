namespace Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability;
    using System;
    using System.Management.Instrumentation;

    [InstrumentationClass(InstrumentationType.Instance)]
    public class DatabaseBlockSetting : ConfigurationSetting
    {
        private string defaultDatabase;

        internal DatabaseBlockSetting(string defaultDatabase)
        {
            this.defaultDatabase = defaultDatabase;
        }

        public string DefaultDatabase
        {
            get
            {
                return this.defaultDatabase;
            }
            internal set
            {
                this.defaultDatabase = value;
            }
        }
    }
}

