namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
    using Microsoft.Practices.ObjectBuilder;
    using System;

    public class ConfiguredObjectStrategy : EnterpriseLibraryBuilderStrategy
    {
        public override object BuildUp(IBuilderContext context, Type t, object existing, string id)
        {
            string name = id;
            IConfigurationSource configurationSource = base.GetConfigurationSource(context);
            ConfigurationReflectionCache reflectionCache = base.GetReflectionCache(context);
            ICustomFactory customFactory = this.GetCustomFactory(t, reflectionCache);
            if (customFactory == null)
            {
                throw new InvalidOperationException(string.Format(Resources.Culture, Resources.ExceptionCustomFactoryAttributeNotFound, new object[] { t.FullName, id }));
            }
            existing = customFactory.CreateObject(context, name, configurationSource, reflectionCache);
            return base.BuildUp(context, t, existing, name);
        }

        private ICustomFactory GetCustomFactory(Type t, ConfigurationReflectionCache reflectionCache)
        {
            return reflectionCache.GetCustomFactory(t);
        }
    }
}

