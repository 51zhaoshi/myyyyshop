namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;
    using System.Configuration;
    using System.Xml;

    public class NamedConfigurationElement : ConfigurationElement, IObjectWithName
    {
        public const string nameProperty = "name";

        public NamedConfigurationElement()
        {
        }

        public NamedConfigurationElement(string name)
        {
            this.Name = name;
        }

        public void DeserializeElement(XmlReader reader)
        {
            base.DeserializeElement(reader, false);
        }

        [StringValidator(MinLength=1), ConfigurationProperty("name", IsKey=true, DefaultValue="Name", IsRequired=true)]
        public string Name
        {
            get
            {
                return (string) base["name"];
            }
            set
            {
                base["name"] = value;
            }
        }
    }
}

