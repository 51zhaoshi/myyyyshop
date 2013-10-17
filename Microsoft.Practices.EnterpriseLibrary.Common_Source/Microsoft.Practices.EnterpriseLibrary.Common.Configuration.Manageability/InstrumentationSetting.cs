namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using System;
    using System.Management.Instrumentation;

    [InstrumentationClass(InstrumentationType.Instance)]
    public class InstrumentationSetting : ConfigurationSetting
    {
        private bool eventLoggingEnabled;
        private bool performanceCountersEnabled;
        private bool wmiEnabled;

        internal InstrumentationSetting(bool eventLoggingEnabled, bool performanceCountersEnabled, bool wmiEnabled)
        {
            this.eventLoggingEnabled = eventLoggingEnabled;
            this.performanceCountersEnabled = performanceCountersEnabled;
            this.wmiEnabled = wmiEnabled;
        }

        public bool EventLoggingEnabled
        {
            get
            {
                return this.eventLoggingEnabled;
            }
            internal set
            {
                this.eventLoggingEnabled = value;
            }
        }

        public bool PerformanceCountersEnabled
        {
            get
            {
                return this.performanceCountersEnabled;
            }
            internal set
            {
                this.performanceCountersEnabled = value;
            }
        }

        public bool WmiEnabled
        {
            get
            {
                return this.wmiEnabled;
            }
            internal set
            {
                this.wmiEnabled = value;
            }
        }
    }
}

