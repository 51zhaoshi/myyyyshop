namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ItemAnchorGetResponse : TopResponse
    {
        [XmlArrayItem("ids_module"), XmlArray("anchor_modules")]
        public List<IdsModule> AnchorModules { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

