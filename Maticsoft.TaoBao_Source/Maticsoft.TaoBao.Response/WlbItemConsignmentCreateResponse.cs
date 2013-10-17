namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbItemConsignmentCreateResponse : TopResponse
    {
        [XmlElement("consignment_id")]
        public long ConsignmentId { get; set; }
    }
}

