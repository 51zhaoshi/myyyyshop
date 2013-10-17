namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ItempropsGetResponse : TopResponse
    {
        [XmlArray("item_props"), XmlArrayItem("item_prop")]
        public List<ItemProp> ItemProps { get; set; }

        [XmlElement("last_modified")]
        public string LastModified { get; set; }
    }
}

