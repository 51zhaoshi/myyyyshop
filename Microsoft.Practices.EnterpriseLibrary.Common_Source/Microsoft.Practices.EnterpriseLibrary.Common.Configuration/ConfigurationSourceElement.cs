namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
    using System;
    using System.Configuration;

    public class ConfigurationSourceElement : NameTypeConfigurationElement
    {
        public ConfigurationSourceElement()
        {
        }

        public ConfigurationSourceElement(string name, Type type) : base(name, type)
        {
        }

        protected internal virtual IConfigurationSource CreateSource()
        {
            throw new ConfigurationErrorsException(Resources.ExceptionBaseConfigurationSourceElementIsInvalid);
        }
    }
}

