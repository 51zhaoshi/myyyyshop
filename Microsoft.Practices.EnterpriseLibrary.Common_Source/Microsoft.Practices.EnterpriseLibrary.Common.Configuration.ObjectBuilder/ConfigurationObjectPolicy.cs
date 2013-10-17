namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.ObjectBuilder;
    using System;

    public class ConfigurationObjectPolicy : IConfigurationObjectPolicy, IBuilderPolicy
    {
        private IConfigurationSource configurationSource;

        public ConfigurationObjectPolicy(IConfigurationSource configurationSource)
        {
            this.configurationSource = configurationSource;
        }

        public IConfigurationSource ConfigurationSource
        {
            get
            {
                return this.configurationSource;
            }
        }
    }
}

