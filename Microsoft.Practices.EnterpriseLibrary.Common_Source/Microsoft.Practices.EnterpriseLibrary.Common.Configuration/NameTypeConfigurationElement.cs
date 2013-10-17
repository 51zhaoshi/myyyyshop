namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;
    using System.Configuration;

    public class NameTypeConfigurationElement : NamedConfigurationElement, IObjectWithNameAndType, IObjectWithName
    {
        private AssemblyQualifiedTypeNameConverter typeConverter;
        public const string typeProperty = "type";

        public NameTypeConfigurationElement()
        {
            this.typeConverter = new AssemblyQualifiedTypeNameConverter();
        }

        public NameTypeConfigurationElement(string name, System.Type type) : base(name)
        {
            this.typeConverter = new AssemblyQualifiedTypeNameConverter();
            this.Type = type;
        }

        internal ConfigurationPropertyCollection Properties
        {
            get
            {
                return base.Properties;
            }
        }

        public System.Type Type
        {
            get
            {
                return (System.Type) this.typeConverter.ConvertFrom(this.TypeName);
            }
            set
            {
                this.TypeName = this.typeConverter.ConvertToString(value);
            }
        }

        [ConfigurationProperty("type", IsRequired=true)]
        public string TypeName
        {
            get
            {
                return (string) base["type"];
            }
            set
            {
                base["type"] = value;
            }
        }
    }
}

