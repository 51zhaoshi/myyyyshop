namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WlbMessage : TopObject
    {
        [XmlElement("gmt_create")]
        public string GmtCreate { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("msg_code")]
        public string MsgCode { get; set; }

        [XmlElement("msg_content")]
        public string MsgContent { get; set; }

        [XmlElement("msg_description")]
        public string MsgDescription { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }
    }
}

