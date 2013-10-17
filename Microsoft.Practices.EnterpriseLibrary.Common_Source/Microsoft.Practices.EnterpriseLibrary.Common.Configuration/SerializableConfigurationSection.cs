namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;
    using System.Configuration;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    public class SerializableConfigurationSection : ConfigurationSection, IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.Read();
            this.DeserializeSection(reader);
        }

        public void WriteXml(XmlWriter writer)
        {
            string data = this.SerializeSection(this, "SerializableConfigurationSection", ConfigurationSaveMode.Full);
            writer.WriteRaw(data);
        }
    }
}

