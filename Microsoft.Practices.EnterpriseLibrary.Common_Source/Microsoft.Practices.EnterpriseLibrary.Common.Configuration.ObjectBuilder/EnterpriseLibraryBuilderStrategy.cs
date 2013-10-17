namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.ObjectBuilder;
    using System;

    public abstract class EnterpriseLibraryBuilderStrategy : BuilderStrategy
    {
        protected EnterpriseLibraryBuilderStrategy()
        {
        }

        protected IConfigurationSource GetConfigurationSource(IBuilderContext context)
        {
            IConfigurationObjectPolicy policy = context.Policies.Get<IConfigurationObjectPolicy>(typeof(IConfigurationSource), null);
            if (policy == null)
            {
                return new SystemConfigurationSource();
            }
            return policy.ConfigurationSource;
        }

        protected ConfigurationReflectionCache GetReflectionCache(IBuilderContext context)
        {
            IReflectionCachePolicy policy = context.Policies.Get<IReflectionCachePolicy>(typeof(IReflectionCachePolicy), null);
            if (policy == null)
            {
                return new ConfigurationReflectionCache();
            }
            return policy.ReflectionCache;
        }
    }
}

