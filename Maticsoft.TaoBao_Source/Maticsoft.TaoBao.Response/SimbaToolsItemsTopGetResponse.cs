namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaToolsItemsTopGetResponse : TopResponse
    {
        [XmlArrayItem("ranked_item"), XmlArray("rankeditems")]
        public List<RankedItem> Rankeditems { get; set; }
    }
}

