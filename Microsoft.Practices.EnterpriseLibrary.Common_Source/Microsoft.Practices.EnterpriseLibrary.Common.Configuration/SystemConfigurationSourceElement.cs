namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;

    public class SystemConfigurationSourceElement : ConfigurationSourceElement
    {
        public SystemConfigurationSourceElement() : this(Resources.SystemConfigurationSourceName)
        {
        }

        public SystemConfigurationSourceElement(string name) : base(name, typeof(SystemConfigurationSource))
        {
        }

        protected internal override IConfigurationSource CreateSource()
        {
            return new SystemConfigurationSource();
        }
    }
}

