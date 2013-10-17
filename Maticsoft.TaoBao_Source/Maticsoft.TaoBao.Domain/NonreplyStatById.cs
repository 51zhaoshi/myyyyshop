namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class NonreplyStatById : TopObject
    {
        [XmlElement("non_reply_customId")]
        public string NonReplyCustomId { get; set; }

        [XmlElement("non_reply_num")]
        public long NonReplyNum { get; set; }

        [XmlElement("service_staff_id")]
        public string ServiceStaffId { get; set; }
    }
}

