namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaAdgroupOnlineitemsGetResponse : TopResponse
    {
        [XmlElement("page_item")]
        public SimbaItemPartition PageItem { get; set; }
    }
}

