namespace Microsoft.Practices.EnterpriseLibrary.Data
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
    using System;

    public class DatabaseMapper : IConfigurationNameMapper
    {
        public string MapName(string name, IConfigurationSource configSource)
        {
            if (name != null)
            {
                return name;
            }
            return new DatabaseConfigurationView(configSource).DefaultName;
        }
    }
}

