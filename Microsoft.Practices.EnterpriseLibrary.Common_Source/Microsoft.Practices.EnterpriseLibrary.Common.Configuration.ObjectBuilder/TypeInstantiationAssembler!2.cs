namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.ObjectBuilder;
    using System;

    public class TypeInstantiationAssembler<TObject, TConfiguration> : IAssembler<TObject, TConfiguration> where TObject: class where TConfiguration: NameTypeConfigurationElement
    {
        public TObject Assemble(IBuilderContext context, TConfiguration objectConfiguration, IConfigurationSource configurationSource, ConfigurationReflectionCache reflectionCache)
        {
            return (TObject) Activator.CreateInstance(objectConfiguration.Type);
        }
    }
}

