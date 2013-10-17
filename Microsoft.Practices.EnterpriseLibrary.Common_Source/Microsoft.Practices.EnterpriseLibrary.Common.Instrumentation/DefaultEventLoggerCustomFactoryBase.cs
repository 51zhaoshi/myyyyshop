namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
    using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation.Configuration;
    using Microsoft.Practices.ObjectBuilder;
    using System;

    public abstract class DefaultEventLoggerCustomFactoryBase : ICustomFactory
    {
        protected DefaultEventLoggerCustomFactoryBase()
        {
        }

        public object CreateObject(IBuilderContext context, string name, IConfigurationSource configurationSource, ConfigurationReflectionCache reflectionCache)
        {
            InstrumentationConfigurationSection configuration = this.GetConfiguration(configurationSource);
            return this.DoCreateObject(configuration);
        }

        protected abstract object DoCreateObject(InstrumentationConfigurationSection instrumentationConfigurationSection);
        private InstrumentationConfigurationSection GetConfiguration(IConfigurationSource configurationSource)
        {
            InstrumentationConfigurationSection section = (InstrumentationConfigurationSection) configurationSource.GetSection("instrumentationConfiguration");
            if (section == null)
            {
                section = new InstrumentationConfigurationSection(false, false, false);
            }
            return section;
        }
    }
}

