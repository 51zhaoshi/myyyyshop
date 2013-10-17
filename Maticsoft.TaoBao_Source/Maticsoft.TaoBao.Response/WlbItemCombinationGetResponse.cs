namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbItemCombinationGetResponse : TopResponse
    {
        [XmlArray("item_id_list"), XmlArrayItem("number")]
        public List<long> ItemIdList { get; set; }
    }
}

