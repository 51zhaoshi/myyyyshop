namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;
    using System.Management.Instrumentation;

    public abstract class InstrumentationListener
    {
        private const string DefaultCounterName = "Total";
        private bool eventLoggingEnabled;
        private IPerformanceCounterNameFormatter nameFormatter;
        private bool performanceCountersEnabled;
        private bool wmiEnabled;

        protected InstrumentationListener(bool performanceCountersEnabled, bool eventLoggingEnabled, bool wmiEnabled, IPerformanceCounterNameFormatter nameFormatter)
        {
            string[] instanceNames = new string[] { "Total" };
            this.Initialize(performanceCountersEnabled, eventLoggingEnabled, wmiEnabled, nameFormatter, instanceNames);
        }

        protected InstrumentationListener(string instanceName, bool performanceCountersEnabled, bool eventLoggingEnabled, bool wmiEnabled, IPerformanceCounterNameFormatter nameFormatter) : this(CreateDefaultInstanceNames(instanceName), performanceCountersEnabled, eventLoggingEnabled, wmiEnabled, nameFormatter)
        {
        }

        protected InstrumentationListener(string[] instanceNames, bool performanceCountersEnabled, bool eventLoggingEnabled, bool wmiEnabled, IPerformanceCounterNameFormatter nameFormatter)
        {
            this.Initialize(performanceCountersEnabled, eventLoggingEnabled, wmiEnabled, nameFormatter, instanceNames);
        }

        private static string[] CreateDefaultInstanceNames(string instanceName)
        {
            return new string[] { "Total", instanceName };
        }

        protected string CreateInstanceName(string nameSuffix)
        {
            return this.nameFormatter.CreateName(nameSuffix);
        }

        protected virtual void CreatePerformanceCounters(string[] instanceNames)
        {
        }

        protected void FireManagementInstrumentation(BaseWmiEvent wmiEvent)
        {
            System.Management.Instrumentation.Instrumentation.Fire(wmiEvent);
        }

        private void FormatCounterInstanceNames(IPerformanceCounterNameFormatter nameFormatter, string[] instanceNames)
        {
            for (int i = 0; i < instanceNames.Length; i++)
            {
                instanceNames[i] = nameFormatter.CreateName(instanceNames[i]);
            }
        }

        protected string GetEventSourceName()
        {
            return ((EventLogDefinitionAttribute) base.GetType().GetCustomAttributes(typeof(EventLogDefinitionAttribute), false)[0]).SourceName;
        }

        private void Initialize(bool performanceCountersEnabled, bool eventLoggingEnabled, bool wmiEnabled, IPerformanceCounterNameFormatter nameFormatter, string[] instanceNames)
        {
            this.performanceCountersEnabled = performanceCountersEnabled;
            this.eventLoggingEnabled = eventLoggingEnabled;
            this.wmiEnabled = wmiEnabled;
            this.nameFormatter = nameFormatter;
            if (performanceCountersEnabled)
            {
                this.FormatCounterInstanceNames(nameFormatter, instanceNames);
                this.CreatePerformanceCounters(instanceNames);
            }
        }

        public bool EventLoggingEnabled
        {
            get
            {
                return this.eventLoggingEnabled;
            }
            protected set
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
            protected set
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
            protected set
            {
                this.wmiEnabled = value;
            }
        }
    }
}

