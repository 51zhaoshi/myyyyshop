namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
    using Microsoft.Practices.ObjectBuilder;
    using System;
    using System.Configuration;

    public abstract class AssemblerBasedCustomFactory<TObject, TConfiguration> : AssemblerBasedObjectFactory<TObject, TConfiguration>, ICustomFactory where TObject: class where TConfiguration: class
    {
        protected AssemblerBasedCustomFactory()
        {
        }

        public TObject Create(IBuilderContext context, string name, IConfigurationSource configurationSource, ConfigurationReflectionCache reflectionCache)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "name");
            }
            TConfiguration configuration = this.GetConfiguration(name, configurationSource);
            if (configuration == null)
            {
                throw new ConfigurationErrorsException(string.Format(Resources.Culture, Resources.ExceptionNamedConfigurationNotFound, new object[] { name, base.GetType().FullName }));
            }
            return this.Create(context, configuration, configurationSource, reflectionCache);
        }

        public object CreateObject(IBuilderContext context, string name, IConfigurationSource configurationSource, ConfigurationReflectionCache reflectionCache)
        {
            return this.Create(context, name, configurationSource, reflectionCache);
        }

        protected abstract TConfiguration GetConfiguration(string name, IConfigurationSource configurationSource);
    }
}

