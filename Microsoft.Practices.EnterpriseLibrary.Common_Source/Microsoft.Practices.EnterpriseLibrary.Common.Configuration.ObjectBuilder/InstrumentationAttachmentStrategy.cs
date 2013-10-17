namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
    using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation.Configuration;
    using System;

    public class InstrumentationAttachmentStrategy
    {
        private InstrumentationAttacherFactory attacherFactory = new InstrumentationAttacherFactory();

        public void AttachInstrumentation(object createdObject, IConfigurationSource configurationSource, ConfigurationReflectionCache reflectionCache)
        {
            ArgumentGenerator arguments = new ArgumentGenerator();
            this.AttachInstrumentation(arguments, createdObject, configurationSource, reflectionCache);
        }

        private void AttachInstrumentation(ArgumentGenerator arguments, object createdObject, IConfigurationSource configurationSource, ConfigurationReflectionCache reflectionCache)
        {
            InstrumentationConfigurationSection configurationSection = this.GetConfigurationSection(configurationSource);
            if (!configurationSection.InstrumentationIsEntirelyDisabled)
            {
                if (createdObject is IInstrumentationEventProvider)
                {
                    createdObject = ((IInstrumentationEventProvider) createdObject).GetInstrumentationEventProvider();
                }
                object[] constructorArgs = arguments.ToArguments(configurationSection);
                this.BindInstrumentationTo(createdObject, constructorArgs, reflectionCache);
            }
        }

        public void AttachInstrumentation(string instanceName, object createdObject, IConfigurationSource configurationSource, ConfigurationReflectionCache reflectionCache)
        {
            ArgumentGenerator arguments = new ArgumentGenerator(instanceName);
            this.AttachInstrumentation(arguments, createdObject, configurationSource, reflectionCache);
        }

        private void BindInstrumentationTo(object createdObject, object[] constructorArgs, ConfigurationReflectionCache reflectionCache)
        {
            this.attacherFactory.CreateBinder(createdObject, constructorArgs, reflectionCache).BindInstrumentation();
        }

        private InstrumentationConfigurationSection GetConfigurationSection(IConfigurationSource configurationSource)
        {
            InstrumentationConfigurationSection section = (InstrumentationConfigurationSection) configurationSource.GetSection("instrumentationConfiguration");
            if (section == null)
            {
                section = new InstrumentationConfigurationSection(false, false, false);
            }
            return section;
        }

        private class ArgumentGenerator
        {
            private string instanceName;

            public ArgumentGenerator()
            {
            }

            public ArgumentGenerator(string instanceName)
            {
                this.instanceName = instanceName;
            }

            public object[] ToArguments(InstrumentationConfigurationSection configSection)
            {
                if (this.instanceName != null)
                {
                    return new object[] { this.instanceName, configSection.PerformanceCountersEnabled, configSection.EventLoggingEnabled, configSection.WmiEnabled };
                }
                return new object[] { configSection.PerformanceCountersEnabled, configSection.EventLoggingEnabled, configSection.WmiEnabled };
            }
        }
    }
}

