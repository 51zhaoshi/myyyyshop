namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbItemMapGetResponse : TopResponse
    {
        [XmlArray("out_entity_item_list"), XmlArrayItem("out_entity_item")]
        public List<OutEntityItem> OutEntityItemList { get; set; }
    }
}

