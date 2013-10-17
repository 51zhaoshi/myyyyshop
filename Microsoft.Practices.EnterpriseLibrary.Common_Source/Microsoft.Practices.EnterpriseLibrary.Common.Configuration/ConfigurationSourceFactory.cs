namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
    using System;
    using System.Configuration;

    public static class ConfigurationSourceFactory
    {
        public static IConfigurationSource Create()
        {
            ConfigurationSourceSection configurationSourceSection = ConfigurationSourceSection.GetConfigurationSourceSection();
            if (configurationSourceSection == null)
            {
                return new SystemConfigurationSource();
            }
            string selectedSource = configurationSourceSection.SelectedSource;
            if (string.IsNullOrEmpty(selectedSource))
            {
                throw new ConfigurationErrorsException(Resources.ExceptionSystemSourceNotDefined);
            }
            return Create(selectedSource);
        }

        public static IConfigurationSource Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            ConfigurationSourceSection configurationSourceSection = ConfigurationSourceSection.GetConfigurationSourceSection();
            if (configurationSourceSection == null)
            {
                throw new ConfigurationErrorsException(Resources.ExceptionConfigurationSourceSectionNotFound);
            }
            ConfigurationSourceElement element = configurationSourceSection.Sources.Get(name);
            if (element == null)
            {
                throw new ConfigurationErrorsException(string.Format(Resources.Culture, Resources.ExceptionNamedConfigurationNotFound, new object[] { name, "ConfigurationSourceFactory" }));
            }
            return element.CreateSource();
        }
    }
}

