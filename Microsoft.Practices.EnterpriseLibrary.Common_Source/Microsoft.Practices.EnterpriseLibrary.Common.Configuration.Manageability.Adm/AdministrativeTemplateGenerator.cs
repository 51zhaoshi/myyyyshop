namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    public static class AdministrativeTemplateGenerator
    {
        public static AdmContent GenerateAdministrativeTemplateContent(IConfigurationSource configurationSource, string applicationName, IDictionary<string, ConfigurationSectionManageabilityProvider> manageabilityProviders)
        {
            AdmContentBuilder contentBuilder = new AdmContentBuilder();
            contentBuilder.StartCategory(applicationName);
            foreach (KeyValuePair<string, ConfigurationSectionManageabilityProvider> pair in manageabilityProviders)
            {
                ConfigurationSection configurationObject = configurationSource.GetSection(pair.Key);
                if (configurationObject != null)
                {
                    pair.Value.AddAdministrativeTemplateDirectives(contentBuilder, configurationObject, configurationSource, applicationName);
                }
            }
            contentBuilder.EndCategory();
            return contentBuilder.GetContent();
        }
    }
}

