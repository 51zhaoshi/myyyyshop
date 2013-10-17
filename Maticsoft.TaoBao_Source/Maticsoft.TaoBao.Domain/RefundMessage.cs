namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class RefundMessage : TopObject
    {
        [XmlElement("content")]
        public string Content { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("message_type")]
        public string MessageType { get; set; }

        [XmlElement("owner_id")]
        public long OwnerId { get; set; }

        [XmlElement("owner_nick")]
        public string OwnerNick { get; set; }

        [XmlElement("owner_role")]
        public string OwnerRole { get; set; }

        [XmlArrayItem("pic_url"), XmlArray("pic_urls")]
        public List<PicUrl> PicUrls { get; set; }

        [XmlElement("refund_id")]
        public long RefundId { get; set; }
    }
}

