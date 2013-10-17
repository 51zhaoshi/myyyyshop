namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ItempropvaluesGetResponse : TopResponse
    {
        [XmlElement("last_modified")]
        public string LastModified { get; set; }

        [XmlArray("prop_values"), XmlArrayItem("prop_value")]
        public List<PropValue> PropValues { get; set; }
    }
}

