namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.ObjectBuilder;

    internal interface IReflectionCachePolicy : IBuilderPolicy
    {
        ConfigurationReflectionCache ReflectionCache { get; }
    }
}

