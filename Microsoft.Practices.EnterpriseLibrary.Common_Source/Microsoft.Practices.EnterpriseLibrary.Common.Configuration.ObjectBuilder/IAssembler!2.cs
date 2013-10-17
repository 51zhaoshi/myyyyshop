namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.ObjectBuilder;

    public interface IAssembler<TObject, TConfiguration> where TObject: class where TConfiguration: class
    {
        TObject Assemble(IBuilderContext context, TConfiguration objectConfiguration, IConfigurationSource configurationSource, ConfigurationReflectionCache reflectionCache);
    }
}

