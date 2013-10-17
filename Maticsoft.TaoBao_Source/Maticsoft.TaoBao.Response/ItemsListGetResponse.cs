namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ItemsListGetResponse : TopResponse
    {
        [XmlArray("items"), XmlArrayItem("item")]
        public List<Item> Items { get; set; }
    }
}

