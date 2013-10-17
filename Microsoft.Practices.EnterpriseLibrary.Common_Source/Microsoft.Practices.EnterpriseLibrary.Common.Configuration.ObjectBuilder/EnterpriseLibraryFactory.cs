namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
    using Microsoft.Practices.ObjectBuilder;
    using System;

    public static class EnterpriseLibraryFactory
    {
        private static IBuilder<BuilderStage> builder = new BuilderBase<BuilderStage>();
        private static ConfigurationReflectionCache reflectionCache = new ConfigurationReflectionCache();

        static EnterpriseLibraryFactory()
        {
            builder.Strategies.AddNew<ConfigurationNameMappingStrategy>(BuilderStage.PreCreation);
            builder.Strategies.AddNew<SingletonStrategy>(BuilderStage.PreCreation);
            builder.Strategies.AddNew<ConfiguredObjectStrategy>(BuilderStage.PreCreation);
            builder.Strategies.AddNew<InstrumentationStrategy>(BuilderStage.PostInitialization);
        }

        public static T BuildUp<T>()
        {
            return BuildUp<T>(ConfigurationSource);
        }

        public static T BuildUp<T>(IConfigurationSource configurationSource)
        {
            return BuildUp<T>((IReadWriteLocator) null, configurationSource);
        }

        public static T BuildUp<T>(IReadWriteLocator locator)
        {
            return BuildUp<T>(locator, ConfigurationSource);
        }

        public static T BuildUp<T>(string id)
        {
            return BuildUp<T>(id, ConfigurationSource);
        }

        public static T BuildUp<T>(IReadWriteLocator locator, IConfigurationSource configurationSource)
        {
            if (configurationSource == null)
            {
                throw new ArgumentNullException("configurationSource");
            }
            return GetObjectBuilder().BuildUp<T>(locator, null, null, new PolicyList[] { GetPolicies(configurationSource) });
        }

        public static T BuildUp<T>(IReadWriteLocator locator, string id)
        {
            return BuildUp<T>(locator, id, ConfigurationSource);
        }

        public static T BuildUp<T>(string id, IConfigurationSource configurationSource)
        {
            return BuildUp<T>(null, id, configurationSource);
        }

        public static T BuildUp<T>(IReadWriteLocator locator, string id, IConfigurationSource configurationSource)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "id");
            }
            if (configurationSource == null)
            {
                throw new ArgumentNullException("configurationSource");
            }
            return GetObjectBuilder().BuildUp<T>(locator, id, null, new PolicyList[] { GetPolicies(configurationSource) });
        }

        private static IBuilder<BuilderStage> GetObjectBuilder()
        {
            return builder;
        }

        private static PolicyList GetPolicies(IConfigurationSource configurationSource)
        {
            PolicyList list = new PolicyList(new PolicyList[0]);
            list.Set<IConfigurationObjectPolicy>(new ConfigurationObjectPolicy(configurationSource), typeof(IConfigurationSource), null);
            list.Set<IReflectionCachePolicy>(new ReflectionCachePolicy(reflectionCache), typeof(IReflectionCachePolicy), null);
            return list;
        }

        private static IConfigurationSource ConfigurationSource
        {
            get
            {
                return ConfigurationSourceFactory.Create();
            }
        }
    }
}

