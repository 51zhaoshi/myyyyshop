namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
    using System;
    using System.Configuration;
    using System.Xml;

    public class NameTypeConfigurationElementCollection<T, TCustomElementData> : PolymorphicConfigurationElementCollection<T> where T: NameTypeConfigurationElement, new() where TCustomElementData: T, new()
    {
        private const string typeAttribute = "type";

        protected override Type RetrieveConfigurationElementType(XmlReader reader)
        {
            Type configurationType = null;
            if (reader.AttributeCount > 0)
            {
                for (bool flag = reader.MoveToFirstAttribute(); flag; flag = reader.MoveToNextAttribute())
                {
                    if ("type".Equals(reader.Name))
                    {
                        Type element = Type.GetType(reader.Value, false);
                        if (element == null)
                        {
                            configurationType = typeof(TCustomElementData);
                        }
                        else
                        {
                            Attribute customAttribute = Attribute.GetCustomAttribute(element, typeof(ConfigurationElementTypeAttribute));
                            if (customAttribute == null)
                            {
                                throw new ConfigurationErrorsException(string.Format(Resources.Culture, Resources.ExceptionNoConfigurationElementAttribute, new object[] { element.Name }));
                            }
                            configurationType = ((ConfigurationElementTypeAttribute) customAttribute).ConfigurationType;
                        }
                        break;
                    }
                }
                if (configurationType == null)
                {
                    throw new ConfigurationErrorsException(string.Format(Resources.Culture, Resources.ExceptionNoTypeAttribute, new object[] { reader.Name }));
                }
                reader.MoveToElement();
            }
            return configurationType;
        }
    }
}

