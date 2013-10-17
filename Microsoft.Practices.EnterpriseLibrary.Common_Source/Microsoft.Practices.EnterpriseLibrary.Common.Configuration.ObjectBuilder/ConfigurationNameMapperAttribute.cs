namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
    using System;

    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
    public sealed class ConfigurationNameMapperAttribute : Attribute
    {
        public Type NameMappingObjectType;

        public ConfigurationNameMapperAttribute(Type nameMappingObjectType)
        {
            if (nameMappingObjectType == null)
            {
                throw new ArgumentNullException("nameMappingObjectType");
            }
            if (!typeof(IConfigurationNameMapper).IsAssignableFrom(nameMappingObjectType))
            {
                throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionTypeNotNameMapper, new object[] { nameMappingObjectType }), "nameMappingObjectType");
            }
            this.NameMappingObjectType = nameMappingObjectType;
        }
    }
}

