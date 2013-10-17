namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using System;

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple=true)]
    public sealed class ConfigurationElementManageabilityProviderAttribute : Attribute
    {
        private Type manageabilityProviderType;
        private Type sectionManageabilityProviderType;
        private Type targetType;

        public ConfigurationElementManageabilityProviderAttribute(Type manageabilityProviderType, Type targetType, Type sectionManageabilityProviderType)
        {
            this.manageabilityProviderType = manageabilityProviderType;
            this.targetType = targetType;
            this.sectionManageabilityProviderType = sectionManageabilityProviderType;
        }

        public Type ManageabilityProviderType
        {
            get
            {
                return this.manageabilityProviderType;
            }
        }

        public Type SectionManageabilityProviderType
        {
            get
            {
                return this.sectionManageabilityProviderType;
            }
        }

        public Type TargetType
        {
            get
            {
                return this.targetType;
            }
        }
    }
}

