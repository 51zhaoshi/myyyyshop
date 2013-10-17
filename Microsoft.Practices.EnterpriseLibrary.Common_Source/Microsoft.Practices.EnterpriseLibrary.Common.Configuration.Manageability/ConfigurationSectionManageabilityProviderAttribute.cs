namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using System;

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple=true)]
    public sealed class ConfigurationSectionManageabilityProviderAttribute : Attribute
    {
        private Type manageabilityProviderType;
        private string sectionName;

        public ConfigurationSectionManageabilityProviderAttribute(string sectionName, Type manageabilityProviderType)
        {
            this.sectionName = sectionName;
            this.manageabilityProviderType = manageabilityProviderType;
        }

        public Type ManageabilityProviderType
        {
            get
            {
                return this.manageabilityProviderType;
            }
        }

        public string SectionName
        {
            get
            {
                return this.sectionName;
            }
        }
    }
}

