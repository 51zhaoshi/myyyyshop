namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TaobaokeReportGetResponse : TopResponse
    {
        [XmlElement("taobaoke_report")]
        public Maticsoft.TaoBao.Domain.TaobaokeReport TaobaokeReport { get; set; }
    }
}

