namespace Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
    using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation.Configuration;
    using System;

    public class DefaultDataEventLoggerCustomFactory : DefaultEventLoggerCustomFactoryBase
    {
        protected override object DoCreateObject(InstrumentationConfigurationSection instrumentationConfigurationSection)
        {
            return new DefaultDataEventLogger(instrumentationConfigurationSection.EventLoggingEnabled, instrumentationConfigurationSection.WmiEnabled);
        }
    }
}

