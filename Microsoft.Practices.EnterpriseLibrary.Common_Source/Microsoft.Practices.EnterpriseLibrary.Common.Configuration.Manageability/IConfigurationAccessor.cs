namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    internal interface IConfigurationAccessor
    {
        IEnumerable<string> GetRequestedSectionNames();
        ConfigurationSection GetSection(string sectionName);
        void RemoveSection(string sectionName);
    }
}

