namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using System;
    using System.Configuration;

    public class InstrumentationConfigurationSection : SerializableConfigurationSection
    {
        private const string eventLoggingEnabled = "eventLoggingEnabled";
        private const string performanceCountersEnabled = "performanceCountersEnabled";
        public const string SectionName = "instrumentationConfiguration";
        private const string wmiEnabled = "wmiEnabled";

        public InstrumentationConfigurationSection()
        {
        }

        public InstrumentationConfigurationSection(bool performanceCountersEnabled, bool eventLoggingEnabled, bool wmiEnabled)
        {
            this.PerformanceCountersEnabled = performanceCountersEnabled;
            this.EventLoggingEnabled = eventLoggingEnabled;
            this.WmiEnabled = wmiEnabled;
        }

        [ConfigurationProperty("eventLoggingEnabled", IsRequired=false, DefaultValue=false)]
        public bool EventLoggingEnabled
        {
            get
            {
                return (bool) base["eventLoggingEnabled"];
            }
            set
            {
                base["eventLoggingEnabled"] = value;
            }
        }

        internal bool InstrumentationIsEntirelyDisabled
        {
            get
            {
                return ((!this.PerformanceCountersEnabled && !this.EventLoggingEnabled) && !this.WmiEnabled);
            }
        }

        [ConfigurationProperty("performanceCountersEnabled", IsRequired=false, DefaultValue=false)]
        public bool PerformanceCountersEnabled
        {
            get
            {
                return (bool) base["performanceCountersEnabled"];
            }
            set
            {
                base["performanceCountersEnabled"] = value;
            }
        }

        [ConfigurationProperty("wmiEnabled", IsRequired=false, DefaultValue=false)]
        public bool WmiEnabled
        {
            get
            {
                return (bool) base["wmiEnabled"];
            }
            set
            {
                base["wmiEnabled"] = value;
            }
        }
    }
}

