namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.ObjectBuilder;
    using System;

    internal class ReflectionCachePolicy : IReflectionCachePolicy, IBuilderPolicy
    {
        private ConfigurationReflectionCache reflectionCache;

        internal ReflectionCachePolicy(ConfigurationReflectionCache reflectionCache)
        {
            this.reflectionCache = reflectionCache;
        }

        public ConfigurationReflectionCache ReflectionCache
        {
            get
            {
                return this.reflectionCache;
            }
        }
    }
}

