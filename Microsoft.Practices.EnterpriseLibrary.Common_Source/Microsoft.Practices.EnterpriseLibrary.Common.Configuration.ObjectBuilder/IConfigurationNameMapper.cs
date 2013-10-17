namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using System;

    public interface IConfigurationNameMapper
    {
        string MapName(string name, IConfigurationSource configSource);
    }
}

