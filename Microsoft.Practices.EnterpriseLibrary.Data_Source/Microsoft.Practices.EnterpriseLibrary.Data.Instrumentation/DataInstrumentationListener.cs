namespace Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
    using Microsoft.Practices.EnterpriseLibrary.Data.Properties;
    using System;
    using System.Diagnostics;

    [HasInstallableResources, PerformanceCountersDefinition("Enterprise Library Data Counters", "CounterCategoryHelpResourceName"), EventLogDefinition("Application", "Enterprise Library Data")]
    public class DataInstrumentationListener : InstrumentationListener
    {
        [PerformanceCounter("Commands Executed/sec", "CommandExecutedCounterHelpResource", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter commandExecutedCounter;
        [PerformanceCounter("Commands Failed/sec", "CommandFailedCounterHelpResource", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter commandFailedCounter;
        [PerformanceCounter("Connections Failed/sec", "ConnectionFailedCounterHelpResource", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter connectionFailedCounter;
        [PerformanceCounter("Connections Opened/sec", "ConnectionOpenedCounterHelpResource", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter connectionOpenedCounter;
        private static EnterpriseLibraryPerformanceCounterFactory counterCache = new EnterpriseLibraryPerformanceCounterFactory();
        private const string counterCategoryName = "Enterprise Library Data Counters";
        private string instanceName;

        public DataInstrumentationListener(string instanceName, bool performanceCountersEnabled, bool eventLoggingEnabled, bool wmiEnabled) : this(instanceName, performanceCountersEnabled, eventLoggingEnabled, wmiEnabled, new AppDomainNameFormatter())
        {
        }

        public DataInstrumentationListener(string instanceName, bool performanceCountersEnabled, bool eventLoggingEnabled, bool wmiEnabled, IPerformanceCounterNameFormatter nameFormatter) : base(instanceName, performanceCountersEnabled, eventLoggingEnabled, wmiEnabled, nameFormatter)
        {
            this.instanceName = instanceName;
        }

        public static void ClearCounterCache()
        {
            counterCache.ClearCachedCounters();
        }

        [InstrumentationConsumer("CommandExecuted")]
        public void CommandExecuted(object sender, CommandExecutedEventArgs e)
        {
            if (base.PerformanceCountersEnabled)
            {
                this.commandExecutedCounter.Increment();
            }
        }

        [InstrumentationConsumer("CommandFailed")]
        public void CommandFailed(object sender, CommandFailedEventArgs e)
        {
            if (base.PerformanceCountersEnabled)
            {
                this.commandFailedCounter.Increment();
            }
            if (base.WmiEnabled)
            {
                base.FireManagementInstrumentation(new CommandFailedEvent(this.instanceName, e.ConnectionString, e.CommandText, e.Exception.ToString()));
            }
        }

        [InstrumentationConsumer("ConnectionFailed")]
        public void ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            if (base.PerformanceCountersEnabled)
            {
                this.connectionFailedCounter.Increment();
            }
            if (base.WmiEnabled)
            {
                base.FireManagementInstrumentation(new ConnectionFailedEvent(this.instanceName, e.ConnectionString, e.Exception.ToString()));
            }
            if (base.EventLoggingEnabled)
            {
                string message = string.Format(Resources.Culture, Resources.ErrorConnectionFailedMessage, new object[] { this.instanceName });
                string str2 = string.Format(Resources.Culture, Resources.ErrorConnectionFailedExtraInformation, new object[] { e.ConnectionString });
                string str3 = new EventLogEntryFormatter(Resources.BlockName).GetEntryText(message, e.Exception, new string[] { str2 });
                EventLog.WriteEntry(base.GetEventSourceName(), str3, EventLogEntryType.Error);
            }
        }

        [InstrumentationConsumer("ConnectionOpened")]
        public void ConnectionOpened(object sender, EventArgs e)
        {
            if (base.PerformanceCountersEnabled)
            {
                this.connectionOpenedCounter.Increment();
            }
        }

        protected override void CreatePerformanceCounters(string[] instanceNames)
        {
            this.connectionOpenedCounter = counterCache.CreateCounter("Enterprise Library Data Counters", "Connections Opened/sec", instanceNames);
            this.commandExecutedCounter = counterCache.CreateCounter("Enterprise Library Data Counters", "Commands Executed/sec", instanceNames);
            this.connectionFailedCounter = counterCache.CreateCounter("Enterprise Library Data Counters", "Connections Failed/sec", instanceNames);
            this.commandFailedCounter = counterCache.CreateCounter("Enterprise Library Data Counters", "Commands Failed/sec", instanceNames);
        }
    }
}

