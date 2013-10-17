namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.ObjectBuilder;

    public interface IConfigurationObjectPolicy : IBuilderPolicy
    {
        IConfigurationSource ConfigurationSource { get; }
    }
}

