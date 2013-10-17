namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.ObjectBuilder;
    using System;

    public class CustomProviderAssembler<TObject, TConfiguration, TConcreteConfiguration> : IAssembler<TObject, TConfiguration> where TObject: class where TConfiguration: class, IObjectWithNameAndType where TConcreteConfiguration: class, TConfiguration, ICustomProviderData
    {
        public TObject Assemble(IBuilderContext context, TConfiguration objectConfiguration, IConfigurationSource configurationSource, ConfigurationReflectionCache reflectionCache)
        {
            TConcreteConfiguration local = (TConcreteConfiguration) objectConfiguration;
            return (TObject) Activator.CreateInstance(objectConfiguration.Type, new object[] { local.Attributes });
        }
    }
}

