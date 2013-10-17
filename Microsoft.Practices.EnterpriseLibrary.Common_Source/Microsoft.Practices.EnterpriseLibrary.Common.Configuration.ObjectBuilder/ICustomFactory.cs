namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.ObjectBuilder;
    using System;

    public interface ICustomFactory
    {
        object CreateObject(IBuilderContext context, string name, IConfigurationSource configurationSource, ConfigurationReflectionCache reflectionCache);
    }
}

