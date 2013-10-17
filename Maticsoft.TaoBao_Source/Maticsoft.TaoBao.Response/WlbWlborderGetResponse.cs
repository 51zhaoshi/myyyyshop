namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbWlborderGetResponse : TopResponse
    {
        [XmlElement("wlb_order")]
        public Maticsoft.TaoBao.Domain.WlbOrder WlbOrder { get; set; }
    }
}

