namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbItemMapGetByExtentityResponse : TopResponse
    {
        [XmlElement("item_id")]
        public long ItemId { get; set; }
    }
}

