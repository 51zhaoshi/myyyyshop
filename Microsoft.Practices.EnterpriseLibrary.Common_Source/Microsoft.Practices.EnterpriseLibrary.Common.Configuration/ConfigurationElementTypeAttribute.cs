namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
    public sealed class ConfigurationElementTypeAttribute : Attribute
    {
        private Type configurationType;

        public ConfigurationElementTypeAttribute()
        {
        }

        public ConfigurationElementTypeAttribute(Type configurationType)
        {
            this.configurationType = configurationType;
        }

        public Type ConfigurationType
        {
            get
            {
                return this.configurationType;
            }
        }
    }
}

